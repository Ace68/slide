## WebAssebly
C'è una storia poco nota in cui sono incappato ultimamente, che parla di Babbo Natale e delle difficoltà che ha dovuto affrontare negli anni per adattarsi all'aumentare delle richieste ricevute dai bambini di tutto il mondo.
Verso la fine degli anni '80, la sua popolarità stava raggiungendo livelli incredibili e si rese necessario studiare un modo nuovo per consentire ali uffici locali degli elfi di comunicare alla sede centrale le lettere dei bambini. Della cosa fu incaricato un certo Berners-Lee, il quale dopo lunghi ragionamenti se ne uscì con HTML, un linguaggio in grado di codificare documenti per la trasmissione via web dei dati su lunga distanza.
All'inizio la cosa sembrò bastare, ma poi i bambini che crescevano volevano poter rendere sempre più speciali e interattive le proprie letterine, e così il giovane Brendan Eich, dell'uffico della Netscape, in preda alla frustrazione dell'ennesima letterina uguale a quella di migliaia di altri bambini decise di inventare JavaScript ... e il mondo non fu mai più lo stesso.
C'è da dire, però, che qua e là qualche manipolo di scalmanati non si riusciva proprio ad accontentare da quanto offerto da JavaScript; avevano letterine scritte scritte anni prima di loro, dai loro genitori e nonni, ed erano bellissime, ma impossibili da rappresentare con HTML e JavaScript.
Gli ingegneri elfici di Google provarono a proporre NaCl, ma volevano che tutti mettessero il loro timbro sui propri scritti. Allora arrivò Mozilla, i cui elfi vollero fare al mondo un regalo straordinario con una tecnologia, asm.js, che era in grado di trasformare qualunque letterina in un contenuto web. C'era solo un problema: le lettere che ne risultavano erano fuori formato ed arrivavano a Babbo Natale fuori tempo massimo.
Nel frattempo arrivammo all'anno 2015, ed il piccolo Brendan Eich era diventato papà e stava vedendo i proprio bambini avere la sua stessa frustrazione di quando era piccolo ... allora decise che era il momento di tornare in azione per trovare una soluzione definitiva. Si rifugiò nel capanno in giardino per 5 giorni e 5 notti, chiese aiuto a qualche amico divenuto importante, e ne uscì con WASM. Finalmente il mondo aveva un modo per trasmettere qualunque tipo di letterina via web, in modo veloce e sicuro!
Quella che vi stiamo per raccontare è l'idea che ha convinto tutti gli elfi di Babbo Natale e dato un futuro alla festa che tutti amiamo!

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

?? Esempio https://squoosh.app/ portato a Google I/O 2019 (https://www.youtube.com/watch?v=njt-Qzw0mVY minuto 10:30) dove hanno usato un encoder C++ per migliorare di molto le prestazioni e qualità ??

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

### Docker
Dopo una paio di twit un pò ambigui da parte del co-founder Solomon Hykes riguardo WebAssembly, Docker pare aver preso una propria strada per il supporto a WebAssembly.
Recentemente è stato annunciato il pieno supporto ai moduli WebAssembly per essere eseguiti direttamente all'interno dei container Docker. Grazie al supporto da parte di Docker a WasmEdge con un singolo comando (docker compose up) sarà possibile buildare, condividere ed eseguire un'applicazione Wasm in un container Docker.
