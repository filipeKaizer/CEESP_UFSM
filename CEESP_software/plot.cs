﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CEESP_software
{
    class plot
    {
        private float centerX;
        private float centerY;
        private float XSValue;

        private float FinalX;
        private float FinalY;
        private bool indutivo;
        private float FP;

        private SolidColorBrush ActualBrush;
        public plot(float centerXv, float centerYv, float Xs)
        {
            this.centerX = centerXv;
            this.centerY = centerYv;
            this.XSValue = Xs;
        }


        public Line createVa(float value)
        {
            // Criar um pincel (brush) usando a cor personalizada
            SolidColorBrush customBrush = new SolidColorBrush(Color.FromRgb(73,172,71));

            Line Va = new Line
            {
                X1 = centerX,
                Y1 = centerY,
                X2 = (centerX + value),
                Y2 = centerY,
                Stroke = customBrush,
                StrokeThickness = 2
            };
            FinalX = (float)Va.X2;
            FinalY = (float)Va.Y2;
            ActualBrush = customBrush;
            return Va; 
        }

        public Line createIa(float value, float FP, bool indutivo)
        {
            value = value * 5;
            float valueIC;
            float angle = (float)Math.Acos(FP);

            SolidColorBrush customBrush = new SolidColorBrush(Color.FromRgb(231, 23, 23));

            if (indutivo) {
                valueIC = value * (float)Math.Sin(angle); //Ou Ia*sub
            } else
            {
                valueIC = (-1) * value * (float)Math.Sin(angle);
            }
            Line Ia = new Line
            {
                X1 = centerX,
                Y1 = centerY,
                X2 = centerX + value * Math.Cos(angle),
                Y2 = centerY + valueIC,
                Stroke = customBrush,
                StrokeThickness = 2
            };
            ActualBrush = customBrush;
            return Ia;
        }

        public Line createXs(float IaValue, float FP)
        {
            SolidColorBrush customBrush = new SolidColorBrush(Color.FromRgb(193, 81, 0));
            float angulo = (float)(Math.Acos(FP));
         //   MessageBox.Show(((XSValue * IaValue) * Math.Cos(1.5708 - angulo)).ToString());

            Line Xs = new Line
            {
                X1 = FinalX,
                Y1 = FinalY,
                X2 = FinalX + XSValue*IaValue*Math.Cos(1.5708-angulo), //Subtrai 90 do angulo.
                Y2 = FinalY - (XSValue*IaValue)*Math.Sin(1.5708-angulo),
                Stroke = customBrush,
                StrokeThickness = 2
            };
            this.FinalX = (float)Xs.X2;
            this.FinalY = (float)Xs.Y2;

            return Xs;
        }

        public Line createEa()
        {
            SolidColorBrush customBrush = new SolidColorBrush(Color.FromRgb(176, 0, 116));

            Line Ea = new Line
            {
                X1 = centerX,
                Y1 = centerY,
                X2 = FinalX,
                Y2 = FinalY,
                Stroke = customBrush,
                StrokeThickness = 2
            };

            return Ea;
        }

        public Path createAngle()
        {
            double angle = 0;
            double radius = 50;
            // Converter ângulo de graus para radianos
            double angleInRadians = angle * Math.PI / 180;

            // Calcular os pontos inicial e final do arco
            double startX = centerX + radius * Math.Cos(angleInRadians / 2);
            double startY = centerY - radius * Math.Sin(angleInRadians / 2);

            double endX = centerX - radius * Math.Cos(angleInRadians / 2);
            double endY = startY;

            PathGeometry pathGeometry = new PathGeometry();
            PathFigure pathFigure = new PathFigure
            {
                StartPoint = new Point(startX, startY),
                IsClosed = false
            };

            ArcSegment arcSegment = new ArcSegment
            {
                Size = new Size(radius, radius),
                IsLargeArc = angle > 180,
                SweepDirection = SweepDirection.Clockwise,
                Point = new Point(endX, endY)
            };

            pathFigure.Segments.Add(arcSegment);
            pathGeometry.Figures.Add(pathFigure);

            SolidColorBrush customBrush = new SolidColorBrush(Color.FromArgb(55, 101, 17, 47));

            Path path = new Path
            {
                Stroke = customBrush,
                StrokeThickness = 2,
                Data = pathGeometry
            };

            return path;
            
        }

        public Polygon createArrow()
        {


            Polygon arrowHead = new Polygon
            {
                Points = new PointCollection { new Point(FinalX, FinalY - 5), new Point(FinalX, FinalY + 5), new Point(FinalX + 10, FinalY) },
                Fill = ActualBrush
            };

            return arrowHead;

        }




    }
}