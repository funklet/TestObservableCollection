using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using TestObservableCollection.Model;
using System.Collections.Concurrent;

namespace TestObservableCollection.Services
{
    public class DataService
    {
        public ObservableConcurrentDictionary<string, TestModel> TestData { get; set; }
        public event NotifyCollectionChangedEventHandler? TestDataCollectionChanged;
        public Timer Timer;
        private int Index = 0;
        public DataService()
        {
            TestData = [];
            TestData.CollectionChanged += OnTestDataCollectionChanged;
            Timer = new Timer(TimerTick, null, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));
        }

        private void TimerTick(object? state)
        {
            ChangeData();
        }

        private void OnTestDataCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            Debug.WriteLine("Collection changed");
            TestDataCollectionChanged?.Invoke(sender, e);
        }

        private void ChangeData()
        {
            //Add Random Data
            var height = new Random().Next(100, 200);
            var age = new Random().Next(1, 100);
            var name = Guid.NewGuid().ToString()[..5];
            var weight = new Random().Next(100, 150);
            TestData.Add(name, new TestModel() { Age = age, Height = height, Weight = weight });

            //Change Random Data
            var keysList = TestData.Keys.ToList();
            var index = new Random().Next(0, TestData.Count());

            var itemToChange = TestData[keysList[index]];

            ChangeItem(itemToChange);


            itemToChange.Age = new Random().Next(1, 100);
            itemToChange.Height = new Random().Next(150, 200);
            itemToChange.Weight = new Random().Next(100, 150);

            //Remove First Data Item if count > 15
            var firstKey = TestData.OrderBy(x => x.Value.WhenAdded).First().Key;

            if (keysList.Count > 15)
            {
                TestData.Remove(firstKey);
            }
        }

        private void ChangeItem(TestModel itemToChange)
        {
            itemToChange.Age = new Random().Next(1, 100);
            itemToChange.Height = new Random().Next(150, 200);
            itemToChange.Weight = new Random().Next(100, 150);
        }
    }
}

