import java.io.FileWriter;
import java.io.IOException;

public class main {
    public static void main(String[] args) {
        if(args.length == 1){
            int res = -1;
            res = tryParseInt(args[0],-1);
            if (res == -1)System.exit(-1);
            try {
                FileWriter myWriter = new FileWriter("OUTPUT.txt");
                myWriter.write("Rozpoczecie obliczen");
                System.out.println("Rozpoczecie obliczen");
                Thread.sleep(10000);
                myWriter.write("Wartosc "+res+" liczby w ciagu fibonaciego jest równa: "+ getFibonacci(res));
                System.out.println("Wartosc "+res+" liczby w ciagu fibonaciego jest równa: "+ getFibonacci(res));
                myWriter.close();
                System.exit(0);
            } catch (IOException | InterruptedException e) {
                System.out.println("An error occurred.");
                e.printStackTrace();
                System.exit(-1);
            }
        }
        System.exit(-1);
    }

    public static int getFibonacci(int nr){
        int a = 0;
        int b = 1;
        int c = 0;

        if(nr ==1)return b;
        for (int i = 2;i<nr; i++){
            c = a+b;
            a = b;
            b = c ;
        }
        return c;
    }

    public static int tryParseInt(String value, int defaultVal) {
        try {
            return Integer.parseInt(value);
        } catch (NumberFormatException e) {
            return defaultVal;
        }
    }
}
