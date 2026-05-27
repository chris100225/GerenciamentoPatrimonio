import Header from "@/components/header/header";
import styles from "./local.module.css";
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
                <section className={`${styles.table_section} ${styles.layout_guide}`} aria-label="Lista de ambientes">
                    <table className={styles.environment_table}>
                        <thead>
                            <tr>
                                <th>
                                    Local
                                </th>
                                <th>
                                    Responsável
                                </th>
                                <th>
                                    Detalhes
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr className="">
                                <td>
                                    Sala 30/31 (anfiteatro)
                                </td>
                                <td>
                                    Samanta Melissa
                                </td>
                                <td>
                                    <a href="#" aria-label="Ver detalhes da Sala 30/31">
                                        <i className="fa-solid fa-circle-info" />
                                    </a>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </section>
                <nav className={styles.pagination} aria-label="Paginação">
                    <button type="button" className={styles.pagination_button} aria-label="Página anterior">
                        ‹
                    </button>
                    <a href="#" className={`${styles.pagination_link} ${styles.current}`} aria-current="page">
                        1
                    </a>
                    <a href="#" className={styles.pagination_link}>
                        2
                    </a>
                    <a href="#" className={styles.pagination_link}>
                        3
                    </a>
                    <button type="button" className={styles.pagination_button} aria-label="Próxima página">
                        ›
                    </button>
                </nav>
            </main>
        </>
    );
};

export default Ambientes;