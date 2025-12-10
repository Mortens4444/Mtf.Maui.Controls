using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Mtf.Maui.Controls.Messages;

public class ShowPage(Page page) : AsyncRequestMessage<Page>
{
    public Page Page { get; set; } = page;
}
