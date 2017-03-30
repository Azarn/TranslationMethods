public class NOD
{
  public static void main(String[] args)
  {
    int a = 50;
	int b = 130;
 
    while(a != 0 && b!=0) {
      if (a > b) {
        	a %= b;
      } else {
        	b %= a;
      }
    }
 
	System.out.println(a + b);
  }
}