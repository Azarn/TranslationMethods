import org.antlr.v4.runtime.*;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.*;

public class RomeNumbers {
    private final static Map<Character, Integer> ROME_SINGLE_CHAR_MAP = new HashMap<>();
    private final static TreeMap<Integer, String> ROME_CHAR_TO_NUMBERS_MAP = new TreeMap<>();
    static {
        ROME_SINGLE_CHAR_MAP.put('I', 1);
        ROME_SINGLE_CHAR_MAP.put('V', 5);
        ROME_SINGLE_CHAR_MAP.put('X', 10);
        ROME_SINGLE_CHAR_MAP.put('L', 50);
        ROME_SINGLE_CHAR_MAP.put('C', 100);
        ROME_SINGLE_CHAR_MAP.put('D', 500);
        ROME_SINGLE_CHAR_MAP.put('M', 1000);

        ROME_CHAR_TO_NUMBERS_MAP.put(4, "IV");
        ROME_CHAR_TO_NUMBERS_MAP.put(9, "IX");
        ROME_CHAR_TO_NUMBERS_MAP.put(40, "XL");
        ROME_CHAR_TO_NUMBERS_MAP.put(90, "XC");
        ROME_CHAR_TO_NUMBERS_MAP.put(400, "CD");
        ROME_CHAR_TO_NUMBERS_MAP.put(900, "CM");
        for(Map.Entry<Character, Integer> entry : ROME_SINGLE_CHAR_MAP.entrySet()) {
            ROME_CHAR_TO_NUMBERS_MAP.put(entry.getValue(), entry.getKey().toString());
        }
    }

    public static int romeToArabic(String s) {
        int res = 0;
        int lastNum = 0;
        for(int i = s.length() - 1; i >= 0; --i) {
            int num = ROME_SINGLE_CHAR_MAP.get(s.charAt(i));
            if (lastNum > num) {
                res -= num;
            } else {
                res += num;
            }
            lastNum = num;
        }
        return res;
    }

    public static String arabicToRome(int n) {
        Map.Entry<Integer, String> entry = ROME_CHAR_TO_NUMBERS_MAP.floorEntry(n);
        int l = ROME_CHAR_TO_NUMBERS_MAP.floorKey(n);
        if (n == l) {
            return entry.getValue();
        }
        return entry.getValue() + arabicToRome(n - l);
    }

    public static void main(String[] args) throws IOException {
        BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
        while(true) {
            String line = br.readLine();
            if (line == null) {
                break;
            }
            ANTLRInputStream in = new ANTLRInputStream(line);
            RomeNumbersLexer lexer = new RomeNumbersLexer(in);
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            RomeNumbersParser parser = new RomeNumbersParser(tokens);
            parser.parse();
        }
    }
}
