﻿using System;
using UnityEngine;
using VitoBarra.GeneralUtility.FeatureFullValue;
using VitoBarra.GridSystem.Framework;

namespace VitoBarra.GridSystem.Square
{
    internal class SquareGridToWord
    {
        private Vector2Hook WordOffset;
        private Vector2 CellNumberOffset;
        private ViewDimension ViewDimension;
        private float TileSize;

        private SquareCell Shape;


        public SquareGridToWord(Vector2Hook wordOffset, float tileSize, Vector2 cellNumberOffset = default,
            ViewDimension viewDimension = ViewDimension.D2)
        {
            WordOffset = wordOffset;
            CellNumberOffset = cellNumberOffset;
            TileSize = tileSize;
            ViewDimension = viewDimension;
        }


        public SquareGridToWord ReShape(int row, int col)
        {
            var newRow = row <= 0 ? 1 : row;
            var newCol = col <= 0 ? 1 : col;
            Shape = new SquareCell(newRow, newCol);
            return this;

        }

        public SquareGridToWord ReShape(SquareCell newShape)
            {
        return ReShape(newShape.Row, newShape.Column);
        }

        public bool IsValidCord(SquareCell cell)
        {
            return cell.Row >= 0 && cell.Row < Shape.Row && cell.Column >= 0 && cell.Column < Shape.Column;
        }

        public SquareGridToWord SetTileSize(float tileSize)
        {
            TileSize = tileSize;
            return this;
        }

        public SquareGridToWord SetCellOffset(Vector2 cellNumberOffset)
        {
            CellNumberOffset = cellNumberOffset;
            return this;
        }

        public SquareGridToWord SetWordOffset(Vector2Hook cellNumberOffset)
        {
            WordOffset = cellNumberOffset;
            return this;
        }

        public SquareGridToWord SetDimension(ViewDimension viewDimension)
        {
            ViewDimension = viewDimension;
            return this;
        }


        public Vector3 GetWordPositionGridEdge(SquareCell cell)
        {
            CalculateWordPosition(out var x, out var y, out var z, cell);
            return new Vector3(x, y, z);
        }

        private void CalculateWordPosition(out float x, out float y, out float z, SquareCell cell, float offset = 0f)
        {
            z = 0;
            y = 0;
            x = (cell.Column + CellNumberOffset.x) * TileSize + WordOffset.i + offset;
            var refAxis = (cell.Row + CellNumberOffset.y) * TileSize + WordOffset.j + offset;
            switch (ViewDimension)
            {
                case ViewDimension.D2:
                    y = refAxis;
                    break;
                case ViewDimension.D3:
                    z = -refAxis;
                    break;
            }
        }

        public Vector3 GetWordPositionCenterCell(SquareCell cell)
        {
            CalculateWordPosition(out var x, out var y, out var z, cell, TileSize / 2);
            return new Vector3(x, y, z);
        }

        public SquareCell GetNearestCell(Vector3 worldPosition)
        {
            var col = Math.Clamp(
                Mathf.FloorToInt((worldPosition.x - CellNumberOffset.x * TileSize - WordOffset.i) / TileSize), 0,
                Shape.Column - 1);
            var refAxis = ViewDimension == ViewDimension.D2 ? worldPosition.y : worldPosition.z;
            var row = Math.Clamp(Mathf.FloorToInt((refAxis - CellNumberOffset.y * TileSize - WordOffset.j) / TileSize),
                0,
                Shape.Row - 1);
            return new SquareCell(row, col);
        }
    }
}