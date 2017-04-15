using System;
using System.Linq;
using Android.Content;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Content.Res;
using System.Collections.Specialized;
using Android.Graphics;
using System.Net;
using System.Collections;
using AndroidGridView = Android.Widget.GridView;
using XamarinDemo.Controls;
using XamarinDemo.Droid.Controls;

[assembly: ExportRenderer(typeof(GridView), typeof(GridViewRenderer))]

namespace XamarinDemo.Droid.Controls
{
    public class GridViewRenderer : ViewRenderer<GridView, AndroidGridView>
    {
        private readonly Orientation _orientation = Orientation.Undefined;

        private GridViewAdapter _adapter;

        protected override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
            if (newConfig.Orientation != _orientation)
                OnElementChanged(new ElementChangedEventArgs<GridView>(Element, Element));
        }

        protected override void OnElementChanged(ElementChangedEventArgs<GridView> e)
        {
            base.OnElementChanged(e);
            var context = Forms.Context;
            var collectionView = new AndroidGridView(context);
            collectionView.SetGravity(GravityFlags.Center);
            collectionView.SetColumnWidth(Convert.ToInt32(Element.ItemWidth));
            collectionView.StretchMode = Android.Widget.StretchMode.StretchColumnWidth;
            var metrics = Resources.DisplayMetrics;
            var spacing = (int)e.NewElement.ColumnSpacing;
            var width = metrics.WidthPixels;
            var itemWidth = (int)e.NewElement.ItemWidth;
            int noOfColumns = width / (itemWidth + spacing);
            // If possible add another row without spacing (because the number of columns will be one less than the number of spacings)
            if (width - (noOfColumns * (itemWidth + spacing)) >= itemWidth)
            {
                noOfColumns++;
            }

            collectionView.SetNumColumns(noOfColumns);
            collectionView.SetHorizontalSpacing(Convert.ToInt32(Element.ColumnSpacing));
            collectionView.SetVerticalSpacing(Convert.ToInt32(Element.RowSpacing));
            Unbind(e.OldElement);
            Bind(e.NewElement);
            _adapter = new GridViewAdapter(context, collectionView, Element);
            collectionView.Adapter = _adapter;
            collectionView.ItemClick += CollectionViewItemClick;
            SetNativeControl(collectionView);
        }

        void CollectionViewItemClick(object sender, Android.Widget.AdapterView.ItemClickEventArgs e)
        {
            var item = Element.ItemsSource.Cast<object>().ElementAt(e.Position);
            Element.InvokeItemSelectedEvent(this, item);
        }

        private void Unbind(GridView oldElement)
        {
            if (oldElement != null)
            {
                oldElement.PropertyChanging -= ElementPropertyChanging;
                oldElement.PropertyChanged -= ElementPropertyChanged;
                if (oldElement.ItemsSource is INotifyCollectionChanged)
                {
                    (oldElement.ItemsSource as INotifyCollectionChanged).CollectionChanged -= DataCollectionChanged;
                }
            }
        }

        private void Bind(GridView newElement)
        {
            if (newElement != null)
            {
                newElement.PropertyChanging += ElementPropertyChanging;
                newElement.PropertyChanged += ElementPropertyChanged;
                if (newElement.ItemsSource is INotifyCollectionChanged)
                {
                    (newElement.ItemsSource as INotifyCollectionChanged).CollectionChanged += DataCollectionChanged;
                }
            }
        }

        private void ElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == GridView.ItemsSourceProperty.PropertyName)
            {
                if (Element.ItemsSource is INotifyCollectionChanged)
                {
                    (Element.ItemsSource as INotifyCollectionChanged).CollectionChanged += DataCollectionChanged;
                    ReloadData();
                }
            }
        }

        private void ElementPropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            if (e.PropertyName == GridView.ItemsSourceProperty.PropertyName)
            {
                if (Element.ItemsSource is INotifyCollectionChanged)
                {
                    (Element.ItemsSource as INotifyCollectionChanged).CollectionChanged -= DataCollectionChanged;
                }
            }
        }

        private void DataCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            ReloadData();
        }

        private void ReloadData()
        {
            if (_adapter != null)
                _adapter.NotifyDataSetChanged();
        }

        public Android.Views.View GetCell(int position, Android.Views.View convertView, ViewGroup parent)
        {
            var item = Element.ItemsSource.Cast<object>().ElementAt(position);
            var viewCellBinded = (Element.ItemTemplate.CreateContent() as ViewCell);
            viewCellBinded.BindingContext = item;
            return CellFactory.GetCell(viewCellBinded, convertView, parent, Context, Element);
        }

        private Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap imageBitmap = null;
            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }

            return imageBitmap;
        }
    }

    public class GridViewAdapter : CellAdapter
    {
        Context _context;
        private readonly AndroidGridView _aView;
        private readonly GridView _view;

        public GridViewAdapter(Context context, AndroidGridView aView, GridView view) : base(context)
        {
            _context = context;
            _aView = aView;
            _view = view;
        }

        public override int Count
        {
            get
            {
                return (_view.ItemsSource as ICollection).Count;
            }
        }

        public override object this[int position]
        {
            get
            {
                var cell = GetCellForPosition(position);
                return cell;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Android.Views.View GetView(int position, Android.Views.View convertView, ViewGroup parent)
        {
            var cell = GetCellForPosition(position);
            var renderer = new GridViewCellRenderer();
            var view = renderer.GetCell(cell, convertView, parent, _context);
            view.LayoutParameters = new Android.Widget.AbsListView.LayoutParams(Convert.ToInt32(_view.ItemWidth),
                Convert.ToInt32(_view.ItemHeight));
            return view;
        }

        protected override Cell GetCellForPosition(int position)
        {
            var item = _view.ItemsSource.Cast<object>().ElementAt(position);
            var cell = (_view.ItemTemplate.CreateContent() as ViewCell);
            cell.BindingContext = item;
            return cell;
        }
    }

    public class GridViewCellRenderer : CellRenderer
    {
        protected override Android.Views.View GetCellCore(Cell item, Android.Views.View convertView, ViewGroup parent, Context context)
        {
            ViewCell viewCell = (ViewCell)item;
            IVisualElementRenderer renderer = Platform.CreateRenderer(viewCell.View);
            return new GridViewCellContainer(context, renderer, viewCell, parent);
        }

        private class GridViewCellContainer : ViewGroup
        {
            private IVisualElementRenderer _renderer;
            private Android.Views.View _parent;
            private ViewCell _viewCell;

            public GridViewCellContainer(Context context, IVisualElementRenderer renderer, ViewCell viewCell, Android.Views.View parent) : base(context)
            {
                _renderer = renderer;
                _parent = parent;
                _viewCell = viewCell;
                AddView(renderer.ViewGroup);
            }

            protected override void OnLayout(bool changed, int l, int t, int r, int b)
            {
                double width = Context.FromPixels(r - l);
                double height = Context.FromPixels(b - t);
                _renderer.Element.Layout(new Rectangle(0, 0, width, height));
                _renderer.UpdateLayout();
            }
        }
    }
}