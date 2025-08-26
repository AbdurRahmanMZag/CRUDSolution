namespace CRUDTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            //arrange
            var mm = new MyMath();
            int input1 = 5; int input2 = 10;//15
            var expected = 15;

            //act
            int actual = mm.Add(input1, input2);

            //assert
            Assert.Equal(expected, actual);
        }
    }
}
