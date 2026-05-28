import Header from "@/components/header/header";
import styles from "./local.module.css";
import Listagem from "@/components/listagem/listagem";
const Ambientes = () => {


    return (
        <>
            <Header />
            <main className={styles.page_content}>
                <section className={`${styles.page_header} ${styles.layout_guide}`} aria-labelledby="titulo-ambientes">
                    <h1 id="titulo-ambientes">
                        Locais
                    </h1>
                    <form className={styles.search_area} role="search">
                        <label htmlFor="pesquisa-ambiente" className={styles.sr_only}>
                            Pesquisar local
                        </label>
                        <input type="search" id="pesquisa-ambiente" name="pesquisaAmbiente" placeholder="Pesquise o ambiente" />
                        <button type="button" className={styles.filter_button} aria-label="Filtrar ambientes">
                            <i className="fa-solid fa-sliders" />
                        </button>
                    </form>
                </section>

                <Listagem page="locais" />
                
            </main>
        </>
    );
};

export default Ambientes;