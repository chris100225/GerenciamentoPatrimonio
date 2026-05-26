import styles from "./header.module.css";

const Header = () => {
    return (

        <header className={styles.topbar}>
            <nav className={styles.navbar + " " + styles.layout_guide} aria-label="Menu principal">
                <a href="#" className={styles.logoLink} aria-label="Página inicial">
                    <img src="../imgs/Logo Senai.png" alt="Logo SENAI" className={styles.logo} />
                </a>
                <ul className={styles.menuList}>
                    <li>
                        <a href="#" className={styles.menuLink}>
                            patrimonios
                            <i className="fa-solid fa-chevron-down" />
                        </a>
                    </li>
                    <li>
                        <a href="#" className={styles.menuLink}>Patrimônios</a>
                    </li>
                </ul>
                <section className={styles.userArea} aria-label="Informações do usuário">
                    <button className={styles.userIcon} aria-label="Abrir perfil do usuário">
                        <i className="fa-solid fa-user" />
                    </button>
                    <div className={styles.userInfo}>
                        <strong>Késsia Milena</strong>
                        <span>kessia@sp.senai.br</span>
                    </div>
                    <button className={styles.arrowButton} aria-label="Abrir opções da conta">
                        <i className="fa-solid fa-chevron-down" />
                    </button>
                </section>
                <button className={styles.hamburguer} aria-label="Abrir opções de menu ">
                    <i className="fa-solid fa-bars" />
                </button>
            </nav>
        </header>);
}
export default Header;