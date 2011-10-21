using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using System.Text;
using System.Diagnostics;
using System.Drawing;
using MonoTouch.CoreAnimation;

namespace IpaziaPDFReader
{
	public partial class PDF_Manager : UIViewController
	{
		
		
		public ScrollViewDelegate scroll_area_delegate;
		public UIView oContentView;
		public CGPDFDocument currentPDFdocument;
		public CGPDFPage currentPDFPage;
		public CATiledLayer oTiledLayer;
		private int page_number;
		public TiledLayerDelegate tiled_layer_delegate;
		
		
		
		//loads the PDF_Manager.xib file and connects it to this object
		public PDF_Manager () : base ("PDF_Manager", null)
		{
			
			
			
		}
		
		public void Init (CGPDFDocument oPdfDoc, int page, PageTurnViewController mother_controller)
		{
			
			this.page_number = page;
			this.currentPDFdocument = oPdfDoc;
			
			
		
			currentPDFPage = this.currentPDFdocument.GetPage (page_number);
			Console.WriteLine ("page: " + page_number);
			
				
			RectangleF oPdfPageRect = this.currentPDFPage.GetBoxRect (CGPDFBox.Crop);
			
			Console.WriteLine ("PDFRect : " + oPdfPageRect.ToString ());
				
			// Setup tiled layer.
			oTiledLayer = new CATiledLayer ();
			tiled_layer_delegate = new TiledLayerDelegate (this);
			oTiledLayer.Delegate = tiled_layer_delegate;
			oTiledLayer.TileSize = new SizeF (1024f, 1024f);
			oTiledLayer.LevelsOfDetail = 5;
			oTiledLayer.LevelsOfDetailBias = 5;
			oTiledLayer.Frame = oPdfPageRect;
			Console.WriteLine ("oTiledLayer.Frame : " + oTiledLayer.Frame.ToString ());
				
			this.oContentView = new UIView (oPdfPageRect);
			this.oContentView.Layer.AddSublayer (oTiledLayer);
			
			//this.View = new UIView (new RectangleF (0, 20, 320, 480 - 20));
			this.View.AutoresizingMask = 
					UIViewAutoresizing.FlexibleWidth
					| UIViewAutoresizing.FlexibleHeight
					| UIViewAutoresizing.FlexibleTopMargin
					| UIViewAutoresizing.FlexibleBottomMargin
					| UIViewAutoresizing.FlexibleLeftMargin
					| UIViewAutoresizing.FlexibleRightMargin;
			this.View.AutosizesSubviews = true;
				

			this.View.Layer.BorderColor = UIColor.Red.CGColor;
			this.View.Layer.BorderWidth = 2f;

				
			
			
			// Prepare scroll view.
			
			this.scroll_area.AutoresizingMask = this.View.AutoresizingMask;
			
			scroll_area_delegate = new ScrollViewDelegate (this);
			scroll_area.Delegate = scroll_area_delegate;	
			
			scroll_area.ContentSize = oPdfPageRect.Size;
			scroll_area.MaximumZoomScale = 1000f;
			scroll_area.MinimumZoomScale = 0.1f;
			
			scroll_area.AddSubview (this.oContentView);
			scroll_area.SetZoomScale (this.View.Frame.Width / oContentView.Frame.Width, false);
			
		}
		
		public override void ViewDidLoad ()
		{

			base.ViewDidLoad ();
		}
		
		
		
		
	}
}
