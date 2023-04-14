using ToDoListMVC.Enums;

namespace ToDoListMVC
{
	public static class DataStorage
	{
		public static string StorageType = StorageTypes.SQL;
		public static void ChangeStorageType()
		{
			StorageType = StorageType == StorageTypes.SQL ? StorageTypes.XML : StorageTypes.SQL;
		}
	}
}
