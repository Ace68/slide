## WebAssebly

Prima di procedere alla scoperta di WebAssembly cerchiamo di capire, ripercorrendo un pò la storia, come mai siamo arrivati ad aver bisogno di WebAssembly.
Correva l'anno 1989 quando Time Berners Lee, insieme a Robert Cailliau, lavorando al CERN, si accorsero che il problema della comunicazione e dello scambio di documenti fra persone che utilizzano sistemi diversi andava risolto con strumenti tecnici, e diedero vita al formato HTML.
Ben presto le pagine statiche divennero un limite alla diffusione del Web, e Netscape decise di asssumere un ingegnere, Brendan Eich, con lo scopo di creare un linguaggio adatto ad essere eseguito nel browser. Detto fatto, nacque JavaScript.
Per un pò di tempo la situazione sembrava essersi assestata, ma nuovamente le esigenze di distribuire di applicazioni complesse via web tornò alla ribalta e Google, nel 2014, propose NativeClient (NaCl) ed il fratello maggiore PNaCl. In effetti questo è stato un grande passo in avanti, perchè Google era riuscita a portare nel browser applicativi scritti con linguaggi di alto livello, e performance nettamente superiori a JavaScript, con un paio di limiti ... era possibile eseguire NaCl solo in Chrome, il browser di casa Google, e si poteva scaricare il plugin solo dall'appstore di Google.
Nell'era della guerra fra browser è la volta di Mozilla che rilancia proponendo asm.js. Questa volta la portabilità è garantita, ma a scapito delle prestazioni.
Il pregio di asm.js è quello però di aver aperto le porte a WebAssembly. Nel 2015 infatti è ancora Brendan Eich ad annunciare una grossa novità, ossia WebAssembly.
Per essere precisi, dice lo stesso Brendan Eich, il progetto era già attivo da un pò, ed era nato con l'idea di essere open source, ma considerando che fra i primi a sostenere l'idea c'erano Microsoft, Google e AutoCAD e c'è voluto un minimo di tempo per trovare i giusti accordi di collaborazione.

Quindi cos'è WebAssembly
Leggendo dal sito ufficiale leggiamo, testualmente "è un formato di istruzioni binarie per una stack-based virtual machine". Chiaro no? Ora, io non so esattamente cos'è una stack-based virtual machine, ma quello che sicuramente mi importa di questa definizione è che si tratta di una virtual machine, ossia un processore, e tutto quanto il necessario, che non esiste realmente, ma che ha il solo scopo di rendere facile la compilazione del codice per svariate architetture reali, e questo per me ha un solo significato "portabilità".
Un esempio? Ho detto poco fa che asm.js è stato fondamentale per la concezione e la realizzazione di WebAssembly, infatti, fra i primi linguaggi ad essere compilati in Wasm ci sono C e C++, ed il compilatore che ha reso possibile questo è Emscripten; ebbene Emscripten originariamente era un compilatore per asm.js.
Forse non avete ben afferrato il peso di quest'ultima frase, ma ho detto che programmi scritti in C, o C++, possono girare ora nel browser. Li scriviamo come sempre, li testiamo sul nostro desktop, e poi li distribuiamo nel web. WebAssembly ha esattamente questo scopo, portare tutti i linguaggi, perchè ormai tutti i linguaggi hanno un compilatore wasm, a poter essere eseguiti nel web.

Ora che abbiamo ripercorso la storia che ci ha condotto a questo punto, ed abbiamo capito il suo scopo, vediamo di capire se veramente abbiamo bisogno di WebAssembly.
Se avete intenzione di mettervi a fare il refactor delle vostre applicazioni per renderle compatibili con WASM, forse prima fareste meglio a porvi qualche domanda.
1. La mia applicazione è su web, e sarà sempre su Web?
2. La mia applicazione è su Desktop, o mobile, e sarà sempre su Desktop (o mobile)?
3. Se avete risposto "sì" solo ad una delle due precedenti domande, allora chiedetevi "la mia applicazione avrà bisogno di essere multipiattaforma"?
4. La mia applicazione ha qualche tipo di dipendenza da librerie di terze parti o di rendering che è difficile da gestire in modo efficiente per JavaScript?

Se avete risposto "sì" alla domanda numero 1, ma no alle altre, potete lasciare questa stanza ... ovviamente scherzavo!
Se avete risposto "sì" alla domanda numero 2, ma no alle altre, potete lasciare questa stanza ... ovviamente scherzavo!
Se avete risposto "Sì" alla domanda numero 3, e "sì" a qualsiasi altra domanda, il vostro progetto potrà beneficiare dall'uso di WASM.

Diciamo subito che WebAssembly non è in realtà un linguaggio, nel senso stretto che noi conosciamo. E' un target di compilazione portatile e condivisibile.
Ma in parole semplici? Significa che WebAssembly è scritto in un altro linguaggio di alto livello, come C, C++, C#, Rust, Java, Python o AssemblyScript e compilato in un bytecode binario, un pò come i linguaggi .NET o Java, ma che può essere consumato dal web.
WebAssembly, infatti, è nativo praticamente su ogni browser. Infatti si tratta di un insieme di APIs per il browser prontamente disponibili a pronte all'uso per ogni browser, il che lo rende estremamente semplice all'uso.

Ma perchè la scelta è caduta proprio su WebAssembly?
Cominciamo col dire che WebAssembly è stato pensato per essere abbinato a JavaScript. Non è mai stato pensato per sostituirlo, e come potrebbe? Hanno lo stesso papà!
E' stato introdotto per dare ai browser un modo per condividere il codice con le loro controparti native ed essere in grado di funzionare ad un livello molto alto per il Web moderno.
Detto questo, le cose vanno comunque risolte nel linguggio migliore per il lavoro che si sta facendo, e a volte, il linguaggio migliore è quello in cui si è più a proprio agio nello scrivere codice.
WebAssembly dovrebbe essere la vostra scelta se state cercando di risolvere problemi che sono più difficili da realizzare con un linguaggio come JavaScript, come, ad esempio, il rendering di immagini, ma semplici da gestire con altri linguaggi.
WebAssembly, a differenza di JavaScript, è già compilato. È un codice ottimizzato fin dall'inizio. In alcuni test con JavaScript e Wasm fianco a fianco, JavaScript esegue le stesse funzioni della sua controparte AssemblyScript e i risultati sono piuttosto sorprendenti. JavaScript è in grado di eseguire i normali cicli For e While senza problemi.
Quando si tratta di costrutti un pò più complessi come la ricorsione e la memoizzazione, AssemblyScript surclassa completamente JavaScript a causa del modo in cui JavaScript gestisce lo stack delle chiamate di funzione.
Cos'è AssemblyScript? E' un nuovo linguaggio, una sorta di TypeScript per WebAssembly.
Poiché Wasm è un codice binario pre-ottimizzato, un enorme vantaggio del suo utilizzo è che funziona benissimo sui dispositivi di fascia bassa, con grande vantaggio per tutto il mondo IoT ad esempio.

### WASI
WASI è l'acronimo di WebAssembly System Interface. E' un'API progettata dal Team di Wasmtime che fornisce l'accesso a diverse funzionalità simili a quelle del sistema operativo, tra cui filesystem, socket Berkeley, clocks di sistema che vengono standardizzati da questa API.
WASI è' progettato per essere indipendente dai browser, quindi non dipende dalle API Web, o da JavaScript e nemmeno è limitato dalla necessità di essere compatibile con JavaScript. Per quanto riguarda la sicurezza, visto che siamo appunto in ambito Web, WASI estende le proprietà della sandbox di WebAssembly all'I/O.

## WAGI
WAGI è l'acronimo di WebAssembly Gateway Interface, che ricorda ai dev un pò più attempati come me, un altro componente del tutto simile, ossia CGI (Common Gateway Interface). Questi due oggetti condividono la stessa idea di base, ossia gestire le richieste HTTP. Il Web Server riceve la chiamata HTTP e avvia un processo per gestirla, in questo caso un processo isolato, quindi un eventuale suo malfunzionamento non compromette la stabilità dell'applicazione.
Il vantaggio di WAGI è che è costruito su Wasmtime, che oltre a garantirci l'isolamento grazie al supporto della sandbox, ci permette di non avere ulteriori framework di mezzo, rendendo il tutto estremamente snello.
