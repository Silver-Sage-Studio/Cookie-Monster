﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Xml;
using System.IO;

namespace Sage_Engine
{
    public class TileMap
    {
        #region Varaibles
        List<TileLayer> layers = new List<TileLayer>();

        int[,] collisionMap;
        #endregion

        #region Properties

        public int[,] CollisionMap
        {
            get
            {
                return collisionMap;
            }
            set
            {
                collisionMap = value;
            }
        }

        /// <summary>
        /// Array list of all the tile layers
        /// </summary>
        public List<TileLayer> Layers
        {
            get
            {
                return layers;
            }
        }


        public int CollisoionMapWidth
        {
            get
            {
                return CollisionMap.GetLength(1);
            }
        }

        public int ColliosnMapHeight
        {
            get
            {
                return CollisionMap.GetLength(0);
            }
        }
        #endregion

        #region Logic
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (TileLayer layer in layers)
            {
                layer.Draw(spriteBatch);
            }
        }
        #endregion

        #region Helper-Methods

        public void SetCollisionTile(int x, int y, int CollisionIndex)
        {
            if ((y >= 0) && (y < ColliosnMapHeight) &&
             (x >= 0) && (x <= CollisoionMapWidth))
                CollisionMap[y, x] = CollisionIndex;
        }


        /// <summary>
        /// Map Width in tiles. (width of largest layer in map)
        /// </summary>
        /// <returns></returns>
        public int MapWidth()
        {
            int max = -10;
            foreach (TileLayer layer in layers)
            {
                max = Math.Max(layer.LayerWidthinTiles, max);
            }

            return max;
        }

        /// <summary>
        /// Map Height in tiles. (Width of largest layer in map).
        /// </summary>
        /// <returns></returns>
        public int MapHeight()
        {
            int max = -10;
            foreach (TileLayer layer in layers)
            {
                max = Math.Max(layer.LayerHeightinTiles, max);
            }

            return max;
        }

        /// <summary>
        /// Add Layers
        /// </summary>
        /// <param name="layer"></param>
        public void Addlayer(TileLayer layer)
        {
            layers.Add(layer);
            Camera.MapHeight = MapHeight() * TileLayer.GetTileHeight;
            Camera.MapWidth = MapWidth() * TileLayer.GetTileWidth;
            modifyCollisionMap();
        }


        //public void SetCellIndex(int x, int y, int Collided)
        //{
        //    if ((y >= 0) && (y < LayerHeightinTiles) &&
        //        (x >= 0) && (x <= LayerWidthinTiles))
        //        layer[y, x] = Index;
        //}


        private void modifyCollisionMap()
        {
            int maxWidth = -1;
            int maxHeight = -1;

            foreach (TileLayer layer in Layers)
            {
                if (layer.LayerWidthinTiles > maxWidth)
                {
                    maxWidth = layer.LayerWidthinTiles;
                }
                if (layer.LayerHeightinTiles > maxHeight)
                {
                    maxHeight = layer.LayerHeightinTiles;
                }
            }


            if (Layers.Count > 0)
            {
                if (CollisionMap != null)
                {
                    int[,] TempMap = (int[,])CollisionMap.Clone();

                    CollisionMap = new int[maxWidth, maxHeight];

                    for (int x = 0; x < Math.Min(maxWidth, TempMap.GetLength(0)); x++)
                    {
                        for (int y = 0; y < Math.Min(maxHeight, TempMap.GetLength(1)); y++)
                        {
                            CollisionMap[x, y] = TempMap[x, y];
                        }
                    }
                }
                else
                {
                    CollisionMap = new int[maxWidth, maxHeight];
                }

            }
            else
            {
                CollisionMap = null;
            }

        }



        /// <summary>
        /// Enter Screen Pixel Co-ordinates to get the index of the tile at the specified co-ordinates.
        /// -1 if no tile at those co-ordinates or other errors, Checks the top layer first, if Transparent, 
        /// Checks the layerBelow it for an Index and so on.
        /// </summary>
        /// <param name="Pixelx"></param>
        /// <param name="PixelY"></param>
        /// <returns></returns>
        public int TileAtPixel(int Pixelx, int PixelY)
        {

            int lastlayer;
            if (layers.Count > 0)
            {
                lastlayer = layers.Count - 1;
            }
            else
            {
                return -1;
            }

            int tileX = Pixelx + (int)Camera.Position.X;
            int tileY = PixelY + (int)Camera.Position.Y;

            tileX = tileX / TileLayer.GetTileWidth;
            tileY = tileX / TileLayer.GetTileHeight;

            int Index;

            do
            {
                TileLayer layer = layers[lastlayer];
                Index = layer.GetCellIndex(tileX, tileY);
                lastlayer = layers.Count - 1;
            }
            while ((Index == -1) && (lastlayer >= 0));

            return Index;
        }

        public void RemoveLayer(TileLayer Layer)
        {
            layers.Remove(Layer);
            modifyCollisionMap();
        }

        public void ReadOutMap(String fileName,
            string Contentpath, string[] TextureNames,
            Dictionary<string, Texture2D> dictTextures,
            string[] LayerName)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode nodeToAdd = doc.CreateElement("TileMap");
            
            for (int i = 0; i < Layers.Count; i++)
            {
                TileLayer layer = Layers[i];
                layer.ReadOutLayer(fileName + (i + 1), Contentpath, TextureNames, dictTextures, LayerName[i]);
            }

            writeOutCollisionMap(fileName + "C");


            for (int i = 0; i < Layers.Count; i++)
            {
                XmlDocument docTemp=new XmlDocument();

                docTemp.Load(String.Concat(fileName, (i + 1)));
                File.Delete(String.Concat(fileName, (i + 1)));
                XmlNode MapNode = docTemp.DocumentElement;
                XmlNode importedNode = nodeToAdd.OwnerDocument.ImportNode(MapNode, true);
                nodeToAdd.AppendChild(importedNode);
            }

            //For CollisionMap
            XmlDocument collDoc = new XmlDocument();
            collDoc.Load(fileName + "C");
            File.Delete(fileName + "C");
            XmlNode CollNode = collDoc.DocumentElement;
            XmlNode ImportedNode = nodeToAdd.OwnerDocument.ImportNode(CollNode, true);
            nodeToAdd.AppendChild(ImportedNode);
            //End CollisionMap Code

            doc.AppendChild(nodeToAdd);


            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            XmlWriter writer = XmlWriter.Create(fileName, settings);

            doc.Save(writer);
            writer.Flush();
            writer.Close();
        }


        private void writeOutCollisionMap(string fileName)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            XmlWriter writer = XmlWriter.Create(fileName, settings);

            writer.WriteStartElement("CollisionMap");
            writer.WriteAttributeString("Width", "" + this.CollisoionMapWidth);
            writer.WriteAttributeString("Height", "" + this.ColliosnMapHeight);

            for (int y = 0; y < this.ColliosnMapHeight; y++)
            {
                writer.WriteStartElement("CollisionRow");
                for (int x = 0; x < this.CollisoionMapWidth; x++)
                {
                    writer.WriteString(" " + collisionMap[y,x] );
                }
                writer.WriteEndElement();// Closing Tag of CollisionRow
            }
            writer.WriteEndElement();// Closing Tag of CollisionMap

            writer.Flush();
            writer.Close();
        }

      


        public static TileMap ReadInMap(string fileName,
        out Dictionary<int, string> textureNames
         )
        {

            textureNames = new Dictionary<int, string>();

            XmlDocument DocToLoad=new XmlDocument();
            DocToLoad.Load(fileName);
            TileMap tempMap= new TileMap();
            XmlNode node = DocToLoad.DocumentElement;

            foreach (XmlNode tempnode in node.ChildNodes) {
                if (tempnode.Name == "TileLayer")
                {
                  
                  TileLayer tempLayer =  TileLayer.ReadInLayer(tempnode, out textureNames);
                  tempMap.Addlayer(tempLayer);
                 }
                else if (tempnode.Name == "CollisionMap")
                {
                     int width, height;
                     width = int.Parse(tempnode.Attributes["Width"].Value);
                     height = int.Parse(tempnode.Attributes["Height"].Value);

                 int[,] CollMap = new int[width, height];

                     int y = 0;

                     foreach (XmlNode RowNode in tempnode.ChildNodes)
                     {
                         string row = RowNode.InnerText;
                         row.Trim();
                         row.TrimStart(' ');
                         string[] CellsInRow = row.Split(' ');

                         for (int x = 1; x < CellsInRow.Length; x++)
                         {
                             CollMap[y, (x - 1)] = int.Parse(CellsInRow[x]);
                         }
                         y++;
                     }

                     tempMap.CollisionMap = CollMap;
                }
            }
           
            return tempMap;
        }
    }
        #endregion
}


