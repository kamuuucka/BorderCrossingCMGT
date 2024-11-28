public class DiscussionHandler : BaseHandler
{
    protected override void OnDeleteData(int id)
    {
        DataPersistenceManager.Instance.DeleteDiscussionSave(id);
    }

    protected override void UseData(int id)
    {
        DataPersistenceManager.Instance.UseTheDiscussionSave(id);
    }

}