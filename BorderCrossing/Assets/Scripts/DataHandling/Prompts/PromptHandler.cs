public class PromptHandler : BaseHandler
{
    protected override void OnDeleteData(int id)
    {
        DataPersistenceManager.Instance.OnDeletePromptSave(id);
    }

    protected override void UseData(int id)
    {
        DataPersistenceManager.Instance.OnSelectThePromptSave(id);
    }

}
