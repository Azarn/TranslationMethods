public class SQRT
{
  public static void main(String[] args)
  {
    int number = 25;
    double t;
	double squareRoot = number / 2;
 
	do {
		t = squareRoot;
		squareRoot = (t + (number / t)) / 2;
	} while ((t - squareRoot) != 0);
    System.out.println(squareRoot);
  }
}
