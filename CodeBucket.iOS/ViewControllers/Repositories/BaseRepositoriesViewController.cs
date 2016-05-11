using CodeBucket.DialogElements;
using CodeBucket.Core.ViewModels.Repositories;
using System;
using UIKit;
using CodeBucket.TableViewCells;
using System.Reactive.Linq;
using System.Linq;
using CodeBucket.Views;

namespace CodeBucket.ViewControllers.Repositories
{
    public abstract class BaseRepositoriesViewController<TViewModel> : ViewModelCollectionDrivenDialogViewController<TViewModel>
        where TViewModel : RepositoriesViewModel
    {
        protected BaseRepositoriesViewController()
        {
            EmptyView = new Lazy<UIView>(() =>
                new EmptyListView(AtlassianIcon.Devtoolsrepository.ToEmptyListImage(), "There are no repositories."));
        }

        public override void ViewDidLoad()
        {
            TableView.RegisterNibForCellReuse(RepositoryCellView.Nib, RepositoryCellView.Key);
            TableView.RowHeight = UITableView.AutomaticDimension;
            TableView.EstimatedRowHeight = 80f;

            base.ViewDidLoad();

            ViewModel.Repositories.Changed
                .Select(_ => ViewModel.Repositories.Select(x => new RepositoryElement(x)))
                .Subscribe(x => Root.Reset(new Section { x }));
        }
    }
}