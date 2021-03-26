using System.Collections.Generic;
using csharpcore;
using NUnit.Framework;

namespace GildedRoseUnitTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// Quality should lower by default Items by 1 per day
        /// </summary>
        [Test]
        public void UpdateQuality_ShouldDegradesNormal_WhenSellByDateIsNotPassed()
        {
            IList<Item> Items = new List<Item> {new Item {Name = "test", SellIn = 5, Quality = 50}};
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.That(Items[0].Quality == 49);
            Assert.That(Items[0].SellIn == 4);
        }

        /// <summary>
        /// Quality should degrade twice as fast when sell by date is passed by default items
        /// </summary>
        [Test]
        public void UpdateQuality_ShouldDegradesTwiceAsFast_WhenSellByDateIsPassed()
        {
            IList<Item> Items = new List<Item> {new Item {Name = "test", SellIn = -1, Quality = 50}};
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.That(Items[0].Quality == 48);
            Assert.That(Items[0].SellIn == -2);
        }

        /// <summary>
        /// Quality shouldnt be negative
        /// </summary>
        [Test]
        public void UpdateQuality_ShouldNeverHaveNegativeQuality_WhenSellByDateIsPassed()
        {
            IList<Item> Items = new List<Item> {new Item {Name = "test", SellIn = -1, Quality = 0}};
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.That(Items[0].Quality == 0);
        }

        /// <summary>
        /// Quality shouldnt be negative even when sellby date is passed
        /// </summary>
        [Test]
        public void UpdateQuality_ShouldNeverHaveNegativeQuality_WhenSellByDateIsDue()
        {
            IList<Item> Items = new List<Item> {new Item {Name = "test", SellIn = 5, Quality = 0}};
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.That(Items[0].Quality == 0);
        }

        /// <summary>
        /// Aged Brie increases in quality
        /// </summary>
        [Test]
        public void UpdateQuality_ShouldIncreaseAgedBrieQuality_WhenSellByDateIsDue()
        {
            IList<Item> Items = new List<Item> {new Item {Name = "Aged Brie", SellIn = 5, Quality = 48}};
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.That(Items[0].Quality == 49);
        }

        /// <summary>
        /// Aged Brie increases twice as fast if date is passed
        /// </summary>
        [Test]
        public void UpdateQuality_ShouldIncreaseAgedBrieQualityTwiceAsFast_WhenSellByDateIsPassed()
        {
            IList<Item> Items = new List<Item> {new Item {Name = "Aged Brie", SellIn = -5, Quality = 48}};
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.That(Items[0].Quality == 50);
        }

        /// <summary>
        /// Quality can never exceed 50, excluding legendary items like sulfuras
        /// </summary>
        [Test]
        public void UpdateQuality_ShouldNotExceed50Quality_WhenQualityIsSetOver50()
        {
            IList<Item> Items = new List<Item> {new Item {Name = "test", SellIn = -5, Quality = 80}};
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.That(Items[0].Quality <= 50);
        }

        /// <summary>
        /// Legendary Items like sulfuras dont have a due date nor decreases in quality
        /// </summary>
        [Test]
        public void UpdateQuality_ShouldNotDecreaseSellInOrQuality_WhenItemIsSulfuras()
        {
            IList<Item> Items = new List<Item>
                {new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 20, Quality = 80}};
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.That(Items[0].Quality == 80);
            Assert.That(Items[0].SellIn == 20);
        }

        /// <summary>
        /// Other Items cant exceed 50 Quality
        /// </summary>
        [Test]
        public void UpdateQuality_ShouldNotExceed50Quality_WhenAgedBrieIsAging()
        {
            IList<Item> Items = new List<Item> {new Item {Name = "Aged Brie", SellIn = -5, Quality = 50}};
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.That(Items[0].Quality <= 50);
        }

        /// <summary>
        /// Backstage items increase in quality
        /// </summary>
        [Test]
        public void UpdateQuality_ShouldIncreaseQuality_WhenItemHasBackStagePass()
        {
            IList<Item> Items = new List<Item>
                {new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 20, Quality = 50}};
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.That(Items[0].Quality == 50);
            Assert.That(Items[0].SellIn == 19);
        }

        /// <summary>
        /// Backstage item increase in quality twice as fast when due date smaller 11
        /// </summary>
        [Test]
        public void UpdateQuality_ShouldIncreaseQualityTwiceAsFast_WhenItemHasBackStagePassAndSellInSmaller11()
        {
            IList<Item> Items = new List<Item>
                {new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 10, Quality = 48}};
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.That(Items[0].Quality == 50);
            Assert.That(Items[0].SellIn == 9);
        }

        /// <summary>
        /// Backstage item increase in quality thrice as fast when due date smaller 6
        /// </summary>
        [Test]
        public void UpdateQuality_ShouldIncreaseQualityThriceAsFast_WhenItemHasBackStagePassAndSellInSmaller6()
        {
            IList<Item> Items = new List<Item>
                {new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 5, Quality = 47}};
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.That(Items[0].Quality == 50);
            Assert.That(Items[0].SellIn == 4);
        }

        /// <summary>
        /// Backstage item cant exceed 50 quality
        /// </summary>
        [Test]
        public void UpdateQuality_ShouldNotExceed50Quality_WhenItemHasBackStagePass()
        {
            IList<Item> Items = new List<Item>
                {new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 5, Quality = 50}};
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.That(Items[0].Quality == 50);
            Assert.That(Items[0].SellIn == 4);
        }

        /// <summary>
        /// Backstage item loses all quality after date passed
        /// </summary>
        [Test]
        public void UpdateQuality_ShouldDropQualityTo0_WhenItemHasBackStagePassAndConcertIsOver()
        {
            IList<Item> Items = new List<Item>
                {new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 0, Quality = 50}};
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.That(Items[0].Quality == 0);
        }

        /// <summary>
        /// Conjured item loses quality twice as fast
        /// </summary>
        [Test]
        public void UpdateQuality_ShouldDegrateTwiceAsFast_WhenItemIsConjured()
        {
            IList<Item> Items = new List<Item> {new Item {Name = "Conjured Mana Cake", SellIn = 10, Quality = 50}};
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.That(Items[0].Quality == 48);
        }

        /// <summary>
        /// conjured item loses quality 4 times faster than normal item when date is passed
        /// </summary>
        [Test]
        public void UpdateQuality_ShouldDegrateFourTimesAsFast_WhenItemIsConjuredSellInDatePassed()
        {
            IList<Item> Items = new List<Item> {new Item {Name = "Conjured Mana Cake", SellIn = 0, Quality = 50}};
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.That(Items[0].Quality == 46);
        }
    }
}