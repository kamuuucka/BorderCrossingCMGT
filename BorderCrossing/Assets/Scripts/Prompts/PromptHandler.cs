public class PromptHandler : BaseHandler
{
    protected override void OnDeleteData(int id)
    {
        DataPersistenceManager.Instance.DeletePromptSave(id);
    }

    protected override void UseData(int id)
    {
        DataPersistenceManager.Instance.UseThePromptSave(id);
    }

}
