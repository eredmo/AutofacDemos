namespace AD11.Infrastructure
{
    public interface IViewModelRunner
    {
        bool? RunViewModelAsDialog(object viewModel);
    }
}
