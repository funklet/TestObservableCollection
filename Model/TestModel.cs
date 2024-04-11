namespace TestObservableCollection.Model
{
    public class TestModel
    {
        public DateTime WhenAdded { get; set; } = DateTime.UtcNow;
        public int Age { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
    }
}
