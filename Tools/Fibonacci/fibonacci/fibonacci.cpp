#include <iostream>
#include <chrono>
#include <thread>
#include <fstream>
using namespace std::chrono_literals;
using namespace std;

string getFibonacci(int nr) {
    int a = 0, b = 1, c = 1;
    for (int i = 2; i < nr; i++) {
        c = a + b;
        a = b;
        b = c;
    }
    return (to_string(c) + ": Jest to"+ to_string(nr) + "liczba ciagu fibonaciego");
}

int getIntFromArg(char* arg) {
    if (strlen(arg) == 0) {
        return -1; //Pusty string
    }
    char* p;
    long lres = strtol(arg, &p, 10);
    if (*p != '\0') {
        return -1; //Znaleziono bledny znak
    }
    if (lres < INT_MIN || lres > INT_MAX) {
        return -1; //Podana wartosc nie miesci sie w zakresie inta
    }
    int res = lres;
    return res;
}

int main(int argc, char** argv)
{
    //Sprawdzenie czy ilosc podanych argumentow jest prawidlowa
    if (argc == 2) {
        int l = getIntFromArg(argv[1]);
        if (l == -1) {
            cout << "Bledny argument" << endl;
            return -1;
        }
        cout << "Rozpoczecie obliczen" << endl;
        ofstream file("OUTPUT.txt");
        file << "Rozpoczecie obliczen" << endl;
        this_thread::sleep_for(10s);
        string res = getFibonacci(l);
        cout << res;
        file << res << endl;
        file.close();
        return 0;
    }
    return -1;
}