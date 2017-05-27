public class NOD
{
  public static void main(String[] args)
  {
    int a;
    a = 50;
    a = a + (3 - 3 + 0) + 60 - 60;
    int b = 130;

    while(a != 0 && b!=0 || true && false) {
      if (a > b) {
          a %= b;
      } else {
          b %= a;
      }
    }

  System.out.println(a + b);
  }
}
