using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.CoreGraphics;

namespace IpaziaPDFReader
{
	public partial class BookPageController : UIViewController
	{
		
		
		private CGPDFDocument currentPDFdocument;
		private PageTurnViewController mother_controller;
		public PDF_Manager pdf_view_controller;
		
		//loads the BookPageController.xib file and connects it to this object
		public BookPageController (int pageIndex, CGPDFDocument oPdfDoc, PageTurnViewController mother_controller) : base ("BookPageController", null)
		{
			
			this.PageIndex = pageIndex;
			this.currentPDFdocument = oPdfDoc;
			
			this.mother_controller = mother_controller;
		}
		
		public int PageIndex {
			get;
			private set;
		}
		
		
		public override void ViewDidLoad ()
		{
			
			base.ViewDidLoad ();
			

			
			this.View.AutoresizingMask = mother_controller.View.AutoresizingMask;
			this.View.AutosizesSubviews = true;
			
			
			Console.WriteLine ("Book page #{0} loaded!", this.PageIndex);
			LoadPDFPage (PageIndex);
			
		}
		
		public void LoadPDFPage (int page_number)
		{
			pdf_view_controller = new PDF_Manager ();
			pdf_view_controller.Init (currentPDFdocument, page_number, mother_controller);
			this.View.AddSubview (pdf_view_controller.View);
			
		}
		
		protected override void Dispose (bool disposing)
		{
			if(pdf_view_controller != null) pdf_view_controller.Dispose ();
			pdf_view_controller = null;
			base.Dispose (disposing);
		}
		
		 ~BookPageController ()
		{
			Console.WriteLine ("BookPageController of page " + PageIndex +"  disposed");
		}
		
	}
}
