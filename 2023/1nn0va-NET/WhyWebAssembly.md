Prima di iniziare, partiamo col dire che Webassembly è una tecnologia giovane ma non giovanissima.

In un'epoca di boom tecnologici guidati da un hype sfrenato, che durano qualche mese o pochi anni e poi svaniscono nel nulla, WA è invece una tecnologia che sta gradualmente guadagnando inerzia, conquistando utilizzatori ed estimatori ogni giorno che passa, dagli amatori a, come vedremo, le grandi multinazionali.

Questo è probabilmente per via del fatto che WA sia una tecnologia nata per risolvere uno specifico set di problemi ma che, col tempo, si è scoperta in grado di migliorare la vita a noi ingegneri in una gamma di situazioni che vanno molto oltre il suo obiettivo iniziale.

Ma iniziamo da un po' di storia...

# Storia
La storia di WebAssembly inizia da molto lontano. Per capire l'esigenza di WebAssembly oggi bisogna capire da dove è partito il tutto.

Nel 1989, un allora giovane ricercatore, Tim Berners-Lee, alla sua seconda borsa di studio al CERN, capì che la soluzione al problema della condivisione
di materiale fra tutti i ricercatori presenti al CERN doveva essere una soluzioone tecnica. Fu così che perfezionò lo studio fatto durante la sua prima 
permanenza al CERN e inventò l'HTML, un linguaggio di markup, in grado di girare all'interno di un'applicazione, il browser, installabile su tutti i sistemi operativi.

La popolarità di HTML esplose velocemente, ed ovviamente tutte le aziende furono ingolosite dalla possibilità di mostrare al mondo i propri prodotti e/o servizi, al punto che
le pagine statiche offerte da HTML cominciarono ad andare un pochino strette ai più esigenti.

Nel 1995 Netscape, produttrice di un browser allora in guerra con Internet Explorer di Microsoft, assunse Bernard Eich, un giovene ingegnere, con l'intento di realizzare
un linguaggio di programmazione che potesse essere eseguito, o meglio interpretato, all'interno del browser. Il risultato è ciò che ancora oggi tutti noi utilizziamo, ossia
JavaScript, e nulla fu più come prima.

Si era di fatto aperta la strada verso le applicazione nel browser, ossia applicazioni usufruibili da tutti i dispositivi in grado di aver installato un browser, indifferentemente dal 
tipo di Sistema Operativo sottostante.

Ben presto però ci si rese conto che le prestazioni offerte da JavaScript non erano le stesse offerte da linguaggi di alto livello quali C o C++. 

*(introdurre qui la differenza tra compilato ed interpretato?)*

La prima a muoversi per cercare di risolvere il problema fu Google che nel 2011 diede origine al progetto Native Client (NaCl), ed in seguito al fratello maggiore PNaCl (Pinnacle), 
proprio con l'intento di consentire a programmi scritti in C/C++ di poter girare all'interno di un browser. Il risultato ottenuto fu tutt'altro che trascurabile. Le prestazioni erano
ottime, ma il tutto poteva girare solo ed sclusivamente nel browser Chrome, e poteva essere scaricato dallo store di Google. Condizioni inaccettabili per la direzione che stava prendendo 
il mercato.

Nel 2013 è la volta di Mozilla che dà origine al progetto asm.js. Questo progetto si dimostrerà un progetto chiave per il successo di WebAssembly. Il vantaggio di asm.js è che è un progetto 
completamente open source e completamente disponibile su tutti i browser, ma, purtroppo, con prestazioni nettamente inferiori a PNaCl.

Proprio quando la situazione sembrava ferma, nel 2015 ecco riapparire il nostro amico Brendan Eich che annuncia al mondo la nascita del progetto open-source WebAssembly.

In verità lo stesso Eich si scusa perchè il progetto era partito qualche mese prima, ma visto le aziende che per prime erano salite a bordo (Microsoft, Google e AutoCad) la firma dei relativi
accordi aveva fatto tardare l'annuncio.

In brevissimo tempo tutte le maggiorni aziende si unirono al progetto e la guerra ad imporre il proprio browser di riferimento convertì nella battaglia per avere uno standard che potesse 
funzionare in tutti i browser. Proprio come, nel lontano 1989, un certo Tim Berners-Lee aveva intuito.

### Quindi cos'è WebAssembly?
Leggendo dal sito ufficiale leggiamo, testualmente "è un formato di istruzioni binarie per una stack-based virtual machine". Chiaro, no? Ora, io non so esattamente cos'è una stack-based virtual machine, ma quello che sicuramente mi importa di questa definizione è che si tratta di una virtual machine, ossia un processore, e tutto quanto il necessario, che non esiste realmente, ma che ha il solo scopo di rendere facile la compilazione del codice per svariate architetture reali, e questo per me ha un solo significato: "portabilità".

Un esempio? Ho detto poco fa che asm.js è stato fondamentale per la concezione e la realizzazione di WebAssembly. Infatti, fra i primi linguaggi ad essere compilati in Wasm ci sono C e C++, ed il compilatore che ha reso possibile questo è Emscripten; ebbene Emscripten originariamente era un compilatore per *asm.js*.

Forse non avete ben afferrato il peso di quest'ultima frase, ma ho detto che programmi scritti in C, o C++, possono girare ora nel browser. Li scriviamo come sempre, li testiamo sul nostro desktop, e poi li distribuiamo nel web. WebAssembly ha esattamente questo scopo: portare tutti i linguaggi, perchè ormai tutti i linguaggi hanno un compilatore wasm, a poter essere eseguiti nel web.

Ora che abbiamo ripercorso la storia che ci ha condotto a questo punto, ed abbiamo capito il suo scopo, vediamo di capire se veramente abbiamo bisogno di WebAssembly.

Se avete intenzione di mettervi a fare il refactor delle vostre applicazioni per renderle compatibili con WASM, forse prima fareste meglio a porvi qualche domanda.
1. La mia applicazione è su web, e sarà sempre su Web?
2. La mia applicazione è su Desktop, o mobile, e sarà sempre su Desktop (o mobile)?
3. Se avete risposto "sì" solo ad una delle due precedenti domande, allora chiedetevi "la mia applicazione avrà bisogno di essere multipiattaforma"?
4. La mia applicazione ha qualche tipo di dipendenza da librerie di terze parti o di rendering che è difficile da gestire in modo efficiente per JavaScript?

Se avete risposto "sì" alla domanda numero 1, ma no alle altre, potete lasciare questa stanza ... ovviamente scherzavo!\
Se avete risposto "sì" alla domanda numero 2, ma no alle altre, potete lasciare questa stanza ... ovviamente scherzavo!\
Se avete risposto "Sì" alla domanda numero 3, e "sì" a qualsiasi altra domanda, il vostro progetto potrà beneficiare dall'uso di WASM.

Diciamo subito che WebAssembly non è in realtà un linguaggio, nel senso stretto che noi conosciamo. E' un target di compilazione portatile e condivisibile.

Ma in parole semplici? Significa che WebAssembly è scritto in un altro linguaggio di alto livello, come C, C++, C#, Rust, Java, Python o AssemblyScript e compilato in un bytecode binario, un pò come i linguaggi .NET o Java, ma che può essere consumato dal web.

WebAssembly, infatti, è nativo praticamente su ogni browser. Infatti si tratta di un insieme di APIs per il browser prontamente disponibili a pronte all'uso per ogni browser, il che lo rende estremamente semplice all'uso.

### Ma perchè la scelta è caduta proprio su WebAssembly?
Cominciamo col dire che WebAssembly è stato pensato per essere abbinato a JavaScript. Non è mai stato pensato per sostituirlo, e come potrebbe? Hanno lo stesso papà!

E' stato introdotto per dare ai browser un modo per condividere il codice con le loro controparti native ed essere in grado di funzionare ad un livello molto alto per il Web moderno.

Detto questo, le cose vanno comunque risolte nel linguggio migliore per il lavoro che si sta facendo e, a volte, il linguaggio migliore è quello in cui si è più a proprio agio nello scrivere codice.

WebAssembly dovrebbe essere la vostra scelta se state cercando di risolvere problemi che sono più difficili da realizzare con un linguaggio come JavaScript come, ad esempio, il rendering di immagini, ma semplici da gestire con altri linguaggi.

WebAssembly, a differenza di JavaScript, è già compilato. È un codice ottimizzato fin dall'inizio. In alcuni test con JavaScript e Wasm fianco a fianco, JavaScript esegue le stesse funzioni della sua controparte AssemblyScript e i risultati sono piuttosto sorprendenti. JavaScript è in grado di eseguire i normali cicli For e While senza problemi.

?? Citazione di come viene eseguito nel browser JS e WA (https://www.youtube.com/watch?v=njt-Qzw0mVY minuto 18:30) ??

Quando si tratta di costrutti un po' più complessi come la ricorsione e la memoizzazione, AssemblyScript surclassa completamente JavaScript a causa del modo in cui JavaScript gestisce lo stack delle chiamate di funzione.

Cos'è AssemblyScript? E' un nuovo linguaggio, una sorta di TypeScript per WebAssembly.\
Poiché Wasm è un codice binario pre-ottimizzato, un enorme vantaggio del suo utilizzo è che funziona benissimo sui dispositivi di fascia bassa, con grande vantaggio per tutto il mondo IoT ad esempio.

### WASI
WASI è l'acronimo di WebAssembly System Interface. E' un'API progettata dal Team di Wasmtime che fornisce l'accesso a diverse funzionalità simili a quelle del sistema operativo, tra cui filesystem, socket Berkeley, clocks di sistema che vengono standardizzati da questa API.

WASI è progettato per essere indipendente dai browser, quindi non dipende dalle API Web, o da JavaScript e nemmeno è limitato dalla necessità di essere compatibile con JavaScript. Per quanto riguarda la sicurezza, visto che siamo appunto in ambito Web, WASI estende le proprietà della sandbox di WebAssembly all'I/O.

### WAGI
WAGI è l'acronimo di WebAssembly Gateway Interface, che ricorda ai dev un pò più attempati come me, un altro componente del tutto simile, ossia CGI (Common Gateway Interface).
WAGI è conforme alle specifiche CGI 1.1 (RFC 3875).
Questi due oggetti condividono la stessa idea di base, ossia gestire le richieste HTTP. Il Web Server riceve la chiamata HTTP e avvia un processo per gestirla, in questo caso un processo isolato, quindi un eventuale suo malfunzionamento non compromette la stabilità dell'applicazione.

Il vantaggio di WAGI è che è costruito su Wasmtime, che oltre a garantirci l'isolamento grazie al supporto della sandbox, ci permette di non avere ulteriori framework di mezzo, rendendo il tutto estremamente snello.

### .NET
La nuova versione del .NET framework ha introdotto diverse novità e miglioramenti per lo sviluppo web. In primis, grazie proprio al supporto di WebAssembly, ora è possibile sviluppare un'applicazione web interamente utilizzando lo stesso linguaggio.
Il miglioramento degli algoritmi di cryptografia consentono ora di salvare informazioni nel browser in modo più sicuro, fermo restando che si tratta sempre di applicazioni che girando appunto nel browser, espongono tutte le informazioni.
L'interoperabilità con JavaScript è notevolmente migliorata, consenentendo ora sia l'esecuzione di codice JavaScript all'interno di C#, ma anche l'esecuzione di codice C# all'interno di JavaScript.
(JSImport e JSExport)
Altra grossa novità è rappresentata dall'introduzione della compilazione AOT (Ahead of Time) che garantisce una velocità di esecuzione nettamente superiore

### Cloud Computing
Quando parliamo di CloudCompouting ci riferiamo, in generale, ad un software che viene eseguito all'interno di un ambiente che è isolato dal sistema operativo host.
Esistono diverse soluzione per questo tipo di architettura

### Virtual Machine
Questa soluzione appartiene alla categoria Pesi Massimi nell'ambito del Cloud Computing.
L'intero sistema operativo viene caricato ed eseguito in un ambiente virtualizzato.
I tempi di avvio si misurano in minuti, le dimensioni delle immagini sono tipicamente di qualche GB.
E' il cavallo da battaglia del Cloud

### Docker Containers
Nella categoria Pesi Medi troviamo i containers.
Negli ultimi anni Docker ha permesso di portare nel cloud un gran numero di server e servizi esistenti in modo portatile e configurabile.
I tempi di avvio è misurato in secondi, le dimensioni delle immagini sono tipicamente misurate in decine di MB.
E' diventato lo standard per incapsulare intere applicazioni che sino ad ora "giravano solo sui PC dei dev".

### WASM
Ciò che abbiamo visto sino ad ora come caratteristiche di WebAssembly, lo rende appetibile per il web, ma contemporaneamente lo rende appetibile anche per il Cloud.
- Cross platform, ma soprattutto cross architecture
- FIle binari di piccole dimensioni
- Gira all'interno di una Sandbox sicura ed isolata
- Tempi di avvio rapidissimi

### Docker e WebAssembly
Possiamo utilizzare WebAssembly per la creazione di famiglie di microservizi, ognuno che svolga un semplice task (AWS Lambda, Azure Functions)
Questo ci sonsente di mantenere il consumo delle risorse al minimo (quando non servono vengono spenti)
Possono scalare rapidamente, in virtù del fatto che un modulo WebAssembly impiega pochi millisecondi ad attivarsi

Possiamo utilizzare Docker per gestire lo stato e abilitare la comunicazione fra i diversi moduli WebAssembly
Questo ci serve per i processi che hanno una lunga durata nel tempo (Saga)
Oppure per processi il cui carico è prevedibile a priori

(Demo di Fermion: https://www.finickywhiskers.com/index.html)
