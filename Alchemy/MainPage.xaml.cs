using Alchemy.Common;
using Alchemy.Controls;
using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Alchemy
{
    public sealed partial class MainPage : Page
    {
        AlchemyEngine _engine = new AlchemyEngine();

        public MainPage()
        {
            this.InitializeComponent();
        }

        private MovableContentControl AddAlchemyElementToGameSurface(AlchemyElement e)
        {
            var mcc = new MovableContentControl() { Width = 100, Height = 115, Content = e, ContentTemplate = Resources["elementDataTemplate"] as DataTemplate };
            mcc.MoveCompleted += mcc_MoveCompleted;
            gameSurface.Children.Add(mcc);
            return mcc;
        }

        private void AddElement(object sender, ItemClickEventArgs e)
        {
            AddAlchemyElementToGameSurface(e.ClickedItem as AlchemyElement);
            this.TopAppBar.IsOpen = false;
        }

        void mcc_MoveCompleted(object sender, EventArgs e)
        {
            var element = sender as MovableContentControl;
            var reactWith = element.IntersectWith();
            if (reactWith.Count() > 1)
            {
                var e1 = reactWith.First();
                var e2 = reactWith.Skip(1).Take(1).First();

                var reaction = _engine.TestForReaction(e1.Content as AlchemyElement, e2.Content as AlchemyElement);
                if (reaction != null)
                {
                    double left = Canvas.GetLeft(e1);
                    double top = Canvas.GetTop(e1);

                    gameSurface.Children.Remove(e1);
                    gameSurface.Children.Remove(e2);

                    var mcc = AddAlchemyElementToGameSurface(reaction);
                    Canvas.SetLeft(mcc, left);
                    Canvas.SetTop(mcc, top);
                }
            }
        }

        private void ElementsAppBar_Opened(object sender, object e)
        {
            elements.ItemsSource = null;
            elements.ItemsSource = _engine.GetUsableElements();
        }
    }
}
