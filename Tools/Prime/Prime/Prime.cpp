#include <iostream>
#include <chrono>
#include <thread>
#include <fstream>
using namespace std::chrono_literals;
using namespace std;

string isPrime(int val) {
    if (val < 2)return (to_string(val) + ": Nie jest liczba pierwsza");
    for (int i = 2; i < (val / 2) + 1; i++) {
        if(val%i == 0)return (to_string(val) + ": Nie jest liczba pierwsza");
    }
    return (to_string(val) + ": Jest liczba pierwsza");
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
        string res = isPrime(l);
        cout << res;
        file << res<<endl;
        file.close();
        return 0;
    }
    return -1;
}

