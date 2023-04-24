namespace ToDoListMVC
{
	public static class DataStorage
	{
		public static string StorageType = "SQL";
		public static void ChangeStorageType(string storageType)
		{
			StorageType = storageType;
		}
	}
}
