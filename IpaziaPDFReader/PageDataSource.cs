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
			if (currentPageController.PageIndex <= 1) {
				// We are on the first page, so there is no need for a controller before that
				return null;
					
			} else {
					
				int previousPageIndex = currentPageController.PageIndex - 1;
				
				//SOLUTION 1
				ForcingPageControllerDispose (currentPageController);
				
				//CREATE NEW PAGE CONTROLLER
				newPageController = new BookPageController (previousPageIndex, parentController.currentPDFdocument, parentController);
				
				//SOLUTION 2
				DisposeThePageControllerWhenDidFinishAnimating (currentPageController, pageViewController);
				
				//SOLUTION 3
				BackupUnusedPagesToAvoidBeingGCd (currentPageController);
				
				return newPageController;
					
			}//end if else					
				
		}
			
		public override UIViewController GetNextViewController (UIPageViewController pageViewController, UIViewController referenceViewController)
		{
				
			currentPageController = referenceViewController as BookPageController;
				
			// Determine if we are on the last page
			if (currentPageController.PageIndex >= (this.parentController.TotalPages)) {
					
				// We are on the last page, so there is no need for a controller after that
				return null;
					
			} else {
					
				int nextPageIndex = currentPageController.PageIndex + 1;
				
				//SOLUTION 1
				ForcingPageControllerDispose (currentPageController);
				
				//CREATE NEW PAGE CONTROLLER
				newPageController = new BookPageController (nextPageIndex, parentController.currentPDFdocument, parentController);
				
				//SOLUTION 2
				DisposeThePageControllerWhenDidFinishAnimating (currentPageController, pageViewController);
				
				//SOLUTION 3
				BackupUnusedPagesToAvoidBeingGCd (currentPageController);
				
				return newPageController;
					
			}//end if else				
				
		}
		
		//SOLUTION 1   
		void ForcingPageControllerDispose (BookPageController oldPageController)
		{
			// --- IF YOU UNCOMMENT THIS, THE CRASHES GOES AWAY, BUT THE PAGE IN THE TRANSITION IS BLANK, SO IS NOT VIABLE
//			currentPageController.View.RemoveFromSuperview ();
//			currentPageController.Dispose ();
		}
		
			
		//SOLUTION 2
		void DisposeThePageControllerWhenDidFinishAnimating (BookPageController oldPageController, UIPageViewController pageViewController)
		{
			// --- IF YOU UNCOMMENT THIS, THE CRASHES STILL HAPPENS
//			pageViewController.DidFinishAnimating += delegate(object sender, UIPageViewFinishedAnimationEventArgs e) {
//				if (currentPageController != null) {
//					currentPageController.View.RemoveFromSuperview ();
//					currentPageController.Dispose ();
//					Console.WriteLine ("currentPageController disposed for page: " + currentPageController.PageIndex);
//				}
//			};
		}
		
		
		//SOLUTION 3
		void BackupUnusedPagesToAvoidBeingGCd (BookPageController oldPageController)
		{
			// --- IF YOU UNCOMMENT THIS, THE CRASHES GOES AWAY, BUT THE PAGES ARE NOT GARBAGE COLLECTED AND AFTER MANY PAGES IPHONE IS OUT OF MEMORY AND IT CRASHES THE APP
			if (parentController.book_page_controllers_reference_list.Contains (currentPageController) == false)
				parentController.book_page_controllers_reference_list.Add (currentPageController);
		}
		
		 ~PageDataSource ()
		{
			Console.WriteLine ("PageDataSource of page " + currentPageController.PageIndex +"  disposed");
		}
			
	}
}

