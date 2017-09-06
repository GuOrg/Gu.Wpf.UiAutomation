namespace Gu.Wpf.UiAutomation
{
    public interface ITreeWalkerFactory
    {
        ITreeWalker GetControlViewWalker();

        ITreeWalker GetContentViewWalker();

        ITreeWalker GetRawViewWalker();

        ITreeWalker GetCustomTreeWalker(ConditionBase condition);
    }
}
