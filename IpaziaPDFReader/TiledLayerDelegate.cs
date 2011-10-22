using System;
using MonoTouch.Foundation;
using System.IO;
using MonoTouch.UIKit;
using System.Text;
using System.Diagnostics;
using System.Drawing;
using MonoTouch.CoreGraphics;
using MonoTouch.CoreAnimation;

namespace IpaziaPDFReader
{
	public class TiledLayerDelegate : CALayerDelegate
	{
			
		public TiledLayerDelegate (PDF_Manager oParentController) : base()
		{
			this.oParentController = oParentController;
		}
				
		private PDF_Manager oParentController;
				
		public override void DrawLayer (CALayer layer, CGContext context)
		{
					
			context.SaveState ();
			context.SetFillColor (1.0f, 1.0f, 1.0f, 1.0f);
			context.FillRect (context.GetClipBoundingBox ());
			context.TranslateCTM (0.0f, layer.Bounds.Size.Height);
			context.ScaleCTM (1.0f, -1.0f);
			context.ConcatCTM (this.oParentController.currentPDFPage.GetDrawingTransform (CGPDFBox.Crop, layer.Bounds, 0, true));
			context.DrawPDFPage (this.oParentController.currentPDFPage);
			context.RestoreState ();	
				
					
		}
		
		 ~TiledLayerDelegate ()
		{
			Console.WriteLine ("TiledLayerDelegate of page " + oParentController.page_number +"  disposed");
		}
	}
}

