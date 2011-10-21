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
	public class ScrollViewDelegate : UIScrollViewDelegate
	{
		public ScrollViewDelegate (PDF_Manager oParentController) : base()
		{
			this.oParentController = oParentController;
		}
				
		private PDF_Manager oParentController;
				
		public override UIView ViewForZoomingInScrollView (UIScrollView scrollView)
		{
			return this.oParentController.oContentView;	
			
		}
	}
}

