using System;
using System.Collections;
using System.Collections.Generic;
using Hexagons;
using UnityEngine;
using UnityEngine.Serialization;

namespace GlobalMap
{
    public class SelectionManager : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private HexGrid hexGrid;
        [SerializeField] private Hex currentHex;
        
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
            
            var hex = _targetHex.GetComponent<Hex>();
            if (!hex) return;
            
            currentHex = hex;

            if (!_neighbours.Contains(currentHex.hexCoords)) return;
            
            foreach (var neighbour in _neighbours)
            {
                hexGrid.GetTile(neighbour).DisableHighlight();
            }
            
            _neighbours = hexGrid.GetNeighbors(currentHex.hexCoords);

            foreach (var neighbour in _neighbours)
            {
                hexGrid.GetTile(neighbour).EnableHighlight();
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