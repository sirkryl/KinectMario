using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Exercise2b
{
    class Game
    {
        private double scrollingSpeed = 0.6;
        private ArrayList groundObjects;
        public ArrayList badObjects;
        public ArrayList goodObjects;
        public ArrayList allObjects;
        private double framerate = 0;
        private double framecount = 0;
        private DateTime gameStartTime;
        private bool createBadObjects=true;
        private bool createGoodObjects = true;
        private double badObjectTime = 6;
        private double goodObjectTime = 10;
        public bool gotHat = false;
        public Point hatPoint;
        private Image hatImg;
        private BitmapImage hatBit;
        Random random = new Random();
        public Game()
        {
            gameStartTime = DateTime.Now;
            groundObjects = new ArrayList();
            badObjects = new ArrayList();
            goodObjects = new ArrayList();
            allObjects = new ArrayList();
            createBadObjects = true;
            createGoodObjects = true;
            for (int i = -100; i <= 1280; i += 48)
            {
                groundObjects.Add(this.MakeShape(0, i, 650f));
            }
            //groundObjects.Add(this.MakeShape(0, 800f, 650f));
            //groundObjects.Add(this.MakeShape(0, 750f, 650f));
            //groundObjects.Add(this.MakeShape(0, 700f, 650f));
            //groundObjects.Add(this.MakeShape(0, 650f, 650f));
            //groundObjects.Add(this.MakeShape(0, 600f, 650f));
            //groundObjects.Add(this.MakeShape(0, 550f, 650f));
            //groundObjects.Add(this.MakeShape(0, 500f, 650f));
            //groundObjects.Add(this.MakeShape(0, 450f, 650f));
            //groundObjects.Add(this.MakeShape(0, 400f, 650f));
            //groundObjects.Add(this.MakeShape(0, 350f, 650f));
            //groundObjects.Add(this.MakeShape(0, 300f, 650f));
            //groundObjects.Add(this.MakeShape(0, 250f, 650f));
            //groundObjects.Add(this.MakeShape(0, 200f, 650f));
            //groundObjects.Add(this.MakeShape(0, 150f, 650f));
            //groundObjects.Add(this.MakeShape(0, 100f, 650f));
            //groundObjects.Add(this.MakeShape(0, 50f, 650f));
            //groundObjects.Add(this.MakeShape(0, 4f, 650f));
            //groundObjects.Add(this.MakeShape(0, -52f, 650f));
            //groundObjects.Add(this.MakeShape(0, -100f, 650f));
            
           
        }
        public void SetFramerate(double actualFramerate)
        {
            framerate = actualFramerate;
        }

        public void SetFrameCount(double frame)
        {
            framecount = frame;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private UIElement MakeShape(int element, double x, double y)
        {
            //var circle = new Ellipse { Width = 10 * 2, Height = 10 * 2, Stroke = new SolidColorBrush(System.Windows.Media.Color.FromScRgb(100, 100, 100, 100)) };


            //circle.StrokeThickness = 1 *  2;
            //circle.Fill = null;
            //circle.SetValue(Canvas.LeftProperty, 200.4);
            //circle.SetValue(Canvas.TopProperty, 44.3);
            //return circle;
            Image simpleImage = new Image();
            

            //  Create source.
            BitmapImage bi = new BitmapImage();
            Uri uri;
            if (element == 0)
            {
                uri = new Uri("/pic/ground.png", UriKind.RelativeOrAbsolute);
                simpleImage.Width = 50;
                simpleImage.Margin = new Thickness(5);
            }
            else if (element == 1)
            {
                uri = new Uri("/pic/badblock.png", UriKind.RelativeOrAbsolute);
                simpleImage.Width = 100;
                simpleImage.Margin = new Thickness(5);
            }
            else
            {
                uri = new Uri("/pic/blue.png", UriKind.RelativeOrAbsolute);
                simpleImage.Width = 100;
                simpleImage.Margin = new Thickness(5);
            }
            
            // BitmapImage.UriSource must be in a BeginInit/EndInit block.
            bi.BeginInit();
            bi.UriSource = uri;
            bi.EndInit();
            // Set the image source.
            //simpleImage.Opacity = 0.8;
            simpleImage.Source = bi;
            simpleImage.SetValue(Canvas.LeftProperty, x);
            simpleImage.SetValue(Canvas.TopProperty, y);
            return simpleImage;

        }
        public void DrawFrame(UIElementCollection children)
        {
            TimeSpan span = DateTime.Now.Subtract(this.gameStartTime);
            int time = span.Seconds;
            
            if (((int)time % 5)==0)
            {
                scrollingSpeed += 0.01;
                //gameStartTime = DateTime.Now;
            }

            //Console.WriteLine(time);
            //Console.WriteLine((int)time+", " + (int)badObjectTime);
            if ((int)time % (int)badObjectTime == 0 )
            {
                if (createBadObjects)
                {
                    badObjectTime -= 0.2;
                    if (badObjectTime < 3)
                    {
                        badObjectTime = 3;
                    }
                    //System.Console.WriteLine(badObjectTime);
                    
                    int rndInt = random.Next(0, 100);
                    if (rndInt <= 40)
                    {
                        allObjects.Add(badObjects[badObjects.Add(this.MakeShape(1, 1280f, 550f))]);
                    }
                    else if (rndInt >= 60)
                    {
                        allObjects.Add(badObjects[badObjects.Add(this.MakeShape(1, 1280f, 100f))]);
                    }
                    else
                    {
                        allObjects.Add(goodObjects[goodObjects.Add(this.MakeShape(2, 1280f, 0f))]);
                    }
                    
                    createBadObjects = false;
                }
            }
            else
            {
                createBadObjects = true;
            }
            for (int i=0;i<groundObjects.Count;i++)
            {
                UIElement element = (UIElement)groundObjects[i];
                double x=(double)element.GetValue(Canvas.LeftProperty);
                x -= scrollingSpeed;
                if (x < -100)
                {
                    groundObjects.Remove(element);
                    groundObjects.Add(this.MakeShape(0, 1280f, 650f));
                    continue;
                }
                
                element.SetValue(Canvas.LeftProperty, x);
                children.Add(element);
            }
            for (int i = 0; i < allObjects.Count; i++)
            {
                UIElement element = (UIElement)allObjects[i];
                double x = (double)element.GetValue(Canvas.LeftProperty);
                x -= scrollingSpeed;
                if (x < 20)
                {
                    if (badObjects.Contains(allObjects[i])) badObjects.Remove(element);
                    else if (goodObjects.Contains(allObjects[i])) goodObjects.Remove(element);
                    allObjects.Remove(element);
                    continue;
                }
                element.SetValue(Canvas.LeftProperty, x);
                children.Add(element);
            }


            if (gotHat)
            {

                if(hatImg == null)
                {
                    hatImg = new Image();
                    hatImg.Width = 80;
                    //  Create source.

                    hatBit = new BitmapImage();
                    Uri uri = new Uri("/pic/hat.png", UriKind.RelativeOrAbsolute);
                    hatBit.BeginInit();
                    hatBit.UriSource = uri;
                    hatBit.EndInit();
                    // Set the image source.
                    
                    //simpleImage.Opacity = 0.8;
                    hatImg.Source = hatBit;
                    
               }
                //hatImg.SetValue(Canvas.LeftProperty, 300.0);//.SetLeft(hatImg, 300f);//this.SkeletonPointToScreen(joint0.Position).X);
                //hatImg.SetValue(Canvas.TopProperty, 300.0);//Canvas.SetTop(hatImg, 300f);//this.SkeletonPointToScreen(joint0.Position).Y);
                hatImg.SetValue(Canvas.LeftProperty, hatPoint.X-50);//.SetLeft(hatImg, 300f);//this.SkeletonPointToScreen(joint0.Position).X);
                hatImg.SetValue(Canvas.TopProperty, hatPoint.Y-50);
                children.Add(hatImg);
            }
        }
    }
}
