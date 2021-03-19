namespace csharpcore
{
    public static class ItemExtension
    {
        private const int InvertValue = -1;
        private const int DoubleSpeed = 2;
        private const int BackStageQualityIncreaseNormal = 1;
        private const int BackStageQualityIncreaseSmallerOrEqual10 = 2;
        private const int BackStageQualityIncreaseSmallerOrEqual5 = 3;
        
        public static void Degenerating(this Item item, int qualityDegenerationFaktor = 1)
        {
            if (item.IsSulfuras()) return;
            if (item.IsAgedBrie()) qualityDegenerationFaktor *= InvertValue;
            if (item.IsConjured()) qualityDegenerationFaktor *= DoubleSpeed;
            item.SellIn -= 1;

            if (item.IsSellByDatePassed())
            {
                item.UpdateItemSellByDatePassed(qualityDegenerationFaktor);
                return;
            }

            if (item.IsBackstage())
            {
                item.UpdateBackStageItem();
                return;
            }
            
            item.Quality -= qualityDegenerationFaktor;
        }

        private static void UpdateItemSellByDatePassed(this Item item, int qualityDegenerationFaktor)
        {
            if (item.IsBackstage())
            {
                item.Quality = 0;
                return;
            }
                
            item.Quality -= qualityDegenerationFaktor * DoubleSpeed;
        }

        private static void UpdateBackStageItem(this Item item)
        {
            if (item.SellIn < 5)
                item.Quality += BackStageQualityIncreaseSmallerOrEqual5;
            else if (item.SellIn < 10)
                item.Quality += BackStageQualityIncreaseSmallerOrEqual10;
            else
                item.Quality += BackStageQualityIncreaseNormal;
        }

        public static void CorrectQualityLimits(this Item item)
        {
            if (item.IsSulfuras())
            {
                return;
            }

            if (item.Quality < 0)
                item.Quality = 0;

            if (item.Quality > 50)
                item.Quality = 50;
        }

        public static bool IsSulfuras(this Item item)
        {
            return item.Name.ToLower().Contains("sulfuras");
        }

        public static bool IsAgedBrie(this Item item)
        {
            return item.Name.ToLower().Contains("aged brie");
        }
        public static bool IsBackstage(this Item item)
        {
            return item.Name.ToLower().Contains("backstage");
        }
        
        public static bool IsConjured(this Item item)
        {
            return item.Name.ToLower().Contains("conjured");
        }

        public static bool IsSellByDatePassed(this Item item)
        {
            return item.SellIn < 0;
        }
    }
}