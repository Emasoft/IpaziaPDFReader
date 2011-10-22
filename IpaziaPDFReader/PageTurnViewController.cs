using MonoTouch.UIKit;
using System.Drawing;
using System;
using MonoTouch.Foundation;
using System.Collections.Generic;
using MonoTouch.CoreGraphics;

namespace IpaziaPDFReader
{
	public partial class PageTurnViewController : UIViewController
	{
		
		
		private int total_book_pages;
		public CGPDFDocument currentPDFdocument;
		public PageDataSource page_data_source;
		private UIPageViewController pageController;
		public BookPageController firstPageController;
		public UIViewController[] page_controllers_array;
		public List<BookPageController> book_page_controllers_reference_list = new List<BookPageController>();
		
		public PageTurnViewController (string nibName, NSBundle bundle) : base (nibName, bundle)
		{
		}
		
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		
		/// <summary>
		/// Gets the total pages in the "Book".
		/// </summary>
		/// <value>
		/// The total pages in the "Book". 
		/// </value>
		public int TotalPages {
			get {
				return total_book_pages;
			}//end get
			
		}//end int TotalPages
		
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			//any additional setup after loading the view, typically from a nib.
			NSUrl u = NSUrl.FromString (@"http://www.eustudies.org/files/pecon_newsletters/PENewsletterSpring07.pdf");
			Console.WriteLine ("Loading PDF: {0}", u.ToString ()); 
			this.currentPDFdocument = CGPDFDocument.FromUrl (u.ToString ());
			total_book_pages = this.currentPDFdocument.Pages;
			Console.WriteLine ("Total Pages: {0}", total_book_pages);
			
			// Initialize the first page
			firstPageController = new BookPageController (1, currentPDFdocument, this);
				
			
			this.pageController = new UIPageViewController (UIPageViewControllerTransitionStyle.PageCurl, 
				UIPageViewControllerNavigationOrientation.Horizontal, UIPageViewControllerSpineLocation.Min);
			
			page_controllers_array = new UIViewController[]{ firstPageController };
			this.pageController.SetViewControllers (page_controllers_array, UIPageViewControllerNavigationDirection.Forward, 
			false, s => { });
			
			page_data_source = new PageDataSource (this);
			this.pageController.DataSource = page_data_source;
			
			this.pageController.View.Frame = this.View.Bounds;
			this.View.AddSubview (this.pageController.View);
									
		}
		
		public override void ViewDidUnload ()
		{
			base.ViewDidUnload ();
			
			// Release any retained subviews of the main view.
			// e.g. myOutlet = null;
		}
		
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			// Return true for supported orientations
			return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
		}
		
		
		

		
	}
}
