import styles from "./card-local.module.css";

type Localizacao = {
    nomeLocal: string;
    area: string;
    responsavel: string;
}

const CardLocal = ({ nomeLocal, area, responsavel }: Localizacao) => {
    return (
        <>
            <table className={styles.environment_table}>
                <tbody>
                    <tr className="">
                        <td>{nomeLocal}</td>
                        <td>{area}</td>
                        <td>{responsavel}</td>
                    </tr>
                </tbody>
            </table>
        </>
    );
}
export default CardLocal;