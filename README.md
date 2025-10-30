# Cattery
Obiettivo

Creare un'applicazione che gestisca le informazioni sui gatti di un gattile
Descrizione del Problema

Si richiede lo sviluppo di un'applicazione per la gestione completa dei gatti presenti in un gattile. L'applicazione dovrà permettere di:

    registrare nuovi gatti,
    visualizzare i gatti presenti,
    gestire le adozioni e gli adottanti
    tenere traccia delle adozioni fallite.

Requisiti Funzionali
Gatto

    Ogni gatto deve avere un codice identificativo univoco generato al momento dell'iscrizione. Questo codice sarà composto da:
    Un numero random di 5 cifre.
    La prima lettera del mese di registrazione.
    L'anno della data di registrazione.
    Tre lettere casuali.

Le informazioni da memorizzare per ogni gatto sono: nome, razza, sesso, data di arrivo al gattile, data di uscita dal gattile (nullable se il gatto è ancora in gattile), data di nascita (nullable se sconosciuta in questo caso si memorizza l'anno presunto di nascita), descrizione
Adozioni

    Per ogni adozione si deve sapere il gatto coinvolto, la data di adozione, l’adottante

Adottante

    CF, nome, cognome, telefono, mail, indirizzo, cap, citta

Memorizzazione Dati

    Tutti i dati devono essere memorizzati in file (JSON)
    I file devono essere aggiornati automaticamente ogni volta che viene inserito un nuovo gatto o gestita una nuova adozione.

Considerazioni importanti per la progettazione

    Separazione delle Responsabilità (SRP): Ogni classe e ogni metodo devono avere una singola responsabilità ben definita.
    Principio Open/Closed (OCP): Le classi dovrebbero essere aperte all'estensione ma chiuse alla modifica.
    Dipendenza da Astrazioni: Favorire l'uso di interfacce o classi astratte per ridurre le dipendenze concrete.

Gestione degli Errori

Prevedere la gestione di eccezioni per situazioni anomale (es. file non trovati, dati malformati).

    diagramma dei casi d'uso
    domain del progetto
    test del domain
    se avete ancora tempo impostare il livello application con dto, mapper, interfacce e usecase
