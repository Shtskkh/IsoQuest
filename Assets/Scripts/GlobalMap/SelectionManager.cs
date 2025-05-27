using System.Collections;
using System.Collections.Generic;
using Hexagons;
using UnityEngine;

namespace GlobalMap
{
    public class SelectionManager : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private HexGrid hexGrid;
        [SerializeField] private Hex currentHex;
        [SerializeField] private Transform player;
        [SerializeField] private int paymentAbility;

        private List<Vector3Int> _neighbours;
        private GameObject _targetHex;

        private void Awake()
        {
            if (!mainCamera)
                mainCamera = Camera.main;
        }

        private IEnumerator Start()
        {
            while (!hexGrid.didStart)
            {
                yield return null;
            }
            
            _neighbours = hexGrid.GetNeighbors(currentHex.hexCoords);
            
            foreach (var neighbour in _neighbours)
            {
                hexGrid.GetTile(neighbour).EnableHighlight();
            }
        }

        public void OnClick(Vector2 inputPosition)
        {
            if (!FindTarget(inputPosition, out _targetHex)) return;

            var targetHex = _targetHex.GetComponent<Hex>();
            if (!targetHex) return;

            if (!_neighbours.Contains(targetHex.hexCoords)) return;

            foreach (var neighbour in _neighbours)
            {
                hexGrid.GetTile(neighbour).DisableHighlight();
            }

            RotatePlayer(targetHex.hexCoords);
            MovePlayer(targetHex.transform.position);
            currentHex = targetHex;

            _neighbours = hexGrid.GetNeighbors(currentHex.hexCoords);

            foreach (var neighbour in _neighbours)
            {
                hexGrid.GetTile(neighbour).EnableHighlight();
            }
        }

        private void MovePlayer(Vector3 position)
        {
            player.position = new Vector3(position.x, HexMetrics.HexHeight, position.z);
        }

        private void RotatePlayer(Vector3Int targetHex)
        {
            var delta = targetHex - currentHex.hexCoords;
            var direction = HexCoords.FromVector3Int(delta);
            if (HexCoords.Directions.TryGetValue(direction, out var targetAngle))
            {
                player.transform.rotation = Quaternion.Euler(0, targetAngle, 0);
            }
        }

        private bool FindTarget(Vector2 inputPosition, out GameObject result)
        {
            var ray = mainCamera.ScreenPointToRay(inputPosition);

            if (Physics.Raycast(ray, out var hit))
            {
                result = hit.collider.gameObject;
                return true;
            }

            result = null;
            return false;
        }
    }
}