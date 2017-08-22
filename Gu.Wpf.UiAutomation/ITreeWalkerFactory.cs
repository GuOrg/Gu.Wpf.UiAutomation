namespace Gu.Wpf.UiAutomation
{
    using Gu.Wpf.UiAutomation.Conditions;

    public interface ITreeWalkerFactory
    {
        ITreeWalker GetControlViewWalker();

        ITreeWalker GetContentViewWalker();

        ITreeWalker GetRawViewWalker();

        ITreeWalker GetCustomTreeWalker(ConditionBase condition);
    }
}
