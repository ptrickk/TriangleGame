﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.MediaFoundation;

namespace TriangleGame.Manager
{
    public class TowerManager
    {
        private List<Tower> _towers;
        private List<Connector> _connectors;
        private List<Area> _areas;

        private List<Ore> _ores;

        public TowerManager()
        {
            _towers = new List<Tower>();
            _connectors = new List<Connector>();
            _areas = new List<Area>();
            _ores = new List<Ore>();
        }

        public List<Tower> Towers
        {
            get => _towers;
        }

        public List<Connector> Connectors
        {
            get => _connectors;
        }

        public List<Area> Areas
        {
            get => _areas;
        }

        public List<Ore> Ores
        {
            get => _ores;
        }

        public void AddTower(Tower tower)
        {
            _towers.Add(tower);
        }

        public void GenerateOres(Point start, Point end, Texture2D texture)
        {
            if (start.X > end.X || start.Y > end.Y)
            {
                throw new Exception("Start and End point are out of position");
            }

            var rand = new Random();
            for (var i = start.X; i < end.X; i++)
            {
                for (var j = start.Y; j < end.Y; j++)
                {
                    if (rand.Next() % 750 == 0)
                    {
                        _ores.Add(new Ore(new Point(i, j), new Point(6, 6), texture, Color.LimeGreen, 200));
                    }
                }
            }
        }
        
        public void Connect(Tower t)
        {
            bool invalid = false;
            foreach (var tower1 in _towers)
            {
                if (t == tower1) continue;

                float distance = Vector2.Distance(t.Position.ToVector2(), tower1.Position.ToVector2());
                if (!(distance > 20) || !(distance < 180)) continue;
                var c1 = new Connector(t, tower1);
                bool intersect = false;
                foreach (var connector in _connectors.Where(connector => c1.Intersects(connector)))
                {
                    intersect = true;
                }

                if (intersect) continue;

                _connectors.Add(c1);
                var dist = _connectors[_connectors.Count - 1].Length;

                for (var i = 0; i < _connectors.Count; i++)
                {
                    Tower t1 = c1.TowerA;
                    Tower t2 = c1.TowerB;
                    Tower t3 = null;

                    for (var j = i + 1; j < _connectors.Count; j++)
                    {
                        Connector c2 = _connectors[j];

                        bool found = false;
                        if (c2.Equals(c1)) continue;

                        if (t1.Equals(c2.TowerA))
                        {
                            t3 = c2.TowerB;
                            found = true;
                        }
                        else if (t1.Equals(c2.TowerB))
                        {
                            t3 = c2.TowerA;
                            found = true;
                        }

                        if (!found) continue;

                        foreach (var c3 in _connectors)
                        {
                            if (!c3.Equals(c1) && !c3.Equals(c2))
                            {
                                //Check das alle drei Tower unique sind
                                if ((!t3.Equals(c3.TowerA) || !t2.Equals(c3.TowerB)) &&
                                    (!t3.Equals(c3.TowerB) || !t2.Equals(c3.TowerA))) continue;


                                /*Sind winkel spitz genug 
                                if (Angle(c1, c2, c3) <= 140 && Angle(c3, c1, c2) <= 140 &&
                                    Angle(c2, c3, c1) <= 140)
                                {}
                                else
                                {
                                    invalid = true;
                                }*/
                                Area area = new Area(c1, c2, c3);
                                _areas.Add(area);
                            }
                        }
                    }
                }
            }

            if (invalid)
            {
                RemoveConnections(t);
                _towers.Remove(t);
            }
        }

        public void RemoveConnections(Tower t)
        {
            for (int i = _connectors.Count - 1; i > 0; i--)
            {
                if (_connectors[i].Contains(t)) _connectors.Remove(_connectors[i]);
            }
        }

        private double Angle(Connector a, Connector b, Connector c)
        {
            double step =
                (Math.Pow(a.Length, 2) + Math.Pow(b.Length, 2) -
                 Math.Pow(c.Length, 2)) / (2 * a.Length * b.Length);
            return Math.Acos(step) * 180 / Math.PI;
        }
    }
}