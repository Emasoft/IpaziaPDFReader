using MonoTouch.UIKit;
using System.Drawing;
using System;
using MonoTouch.Foundation;
using System.Collections.Generic;
using MonoTouch.CoreGraphics;

namespace IpaziaPDFReader
{
	public class PageDataSource : UIPageViewControllerDataSource
	{
			
		public BookPageController currentPageController;
		public BookPageController newPageController;
			
		public PageDataSource (PageTurnViewController parentController)
		{
			this.parentController = parentController;
		}
		
		private PageTurnViewController parentController;
			
		public override UIViewController GetPreviousViewController (UIPageViewController pageViewController, UIViewController referenceViewController)
		{
				
			currentPageController = referenceViewController as BookPageController;
				
			// Determine if we are on the first page
			if (currentPageController.PageIndex <= 0) {
				// We are on the first page, so there is no need for a controller before that
				return null;
					
			} else {
					
				int previousPageIndex = currentPageController.PageIndex - 1;
				newPageController = new BookPageController (previousPageIndex, parentController.currentPDFdocument, parentController);
				return newPageController;
					
			}//end if else					
				
		}
			
		public override UIViewController GetNextViewController (UIPageViewController pageViewController, UIViewController referenceViewController)
		{
				
			currentPageController = referenceViewController as BookPageController;
				
			// Determine if we are on the last page
			if (currentPageController.PageIndex >= (this.parentController.TotalPages - 1)) {
					
				// We are on the last page, so there is no need for a controller after that
				return null;
					
			} else {
					
				int nextPageIndex = currentPageController.PageIndex + 1;
				
				newPageController = new BookPageController (nextPageIndex, parentController.currentPDFdocument, parentController);
				return newPageController;
					
			}//end if else				
				
		}
			
	}
}

