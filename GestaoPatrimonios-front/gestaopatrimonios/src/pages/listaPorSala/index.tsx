// import styles from "./listaPorSala.module.css";

// const ListaPorSala = () => {
//     return (
//         <>

//             <main className="page-content">
//                 <section className="page-header layout_guide" aria-labelledby="titulo-patrimonios">
//                     <h1 id="titulo-patrimonios">Patrimônios: Sala 09/10</h1>
//                     <form className="search-area" role="search">
//                         <label htmlFor="pesquisa-ambiente" className="sr-only">Pesquisar patrimônios</label>
//                         <input type="search" id="pesquisa-ambiente" name="pesquisaAmbiente" placeholder="Pesquise o ambiente" />
//                         <button type="button" className="filter-button" aria-label="Filtrar patrimonios">
//                             <i className="fa-solid fa-sliders" />
//                         </button>
//                     </form>
//                 </section>
//                 <section className="table-section layout_guide" aria-label="Lista de patrimonios">
//                     <table className="environment-table">
//                         <thead>
//                             <tr>
//                                 <th>Patrimônio</th>
//                                 <th>Denominação</th>
//                                 <th>Tipo</th>
//                                 <th>Data transfêrencia</th>
//                                 <th>Detalhes</th>
//                                 <th>Transferir</th>
//                             </tr>
//                         </thead>
//                         <tbody>
//                             <tr className>
//                                 <td>1236808</td>
//                                 <td>MESA TRAPEZOIDAL DC-1987a</td>
//                                 <td>Mesa</td>
//                                 <td>11/02/26</td>
//                                 <td>
//                                     <a href="#" aria-label="Ver detalhes do patrimonio">
//                                         <i className="fa-solid fa-circle-info" />
//                                     </a>
//                                 </td>
//                                 <td>
//                                     <a href="#" aria-label="Transferir patrimonio">
//                                         <i className="fa-solid fa-arrow-right-arrow-left" />
//                                     </a>
//                                 </td>
//                             </tr>
//                         </tbody>
//                     </table>
//                 </section>
//                 <nav className="pagination" aria-label="Paginação">
//                     <button type="button" className="pagination-button" aria-label="Página anterior">
//                         ‹
//                     </button>
//                     <a href="#" className="pagination-link current" aria-current="page">1</a>
//                     <a href="#" className="pagination-link">2</a>
//                     <a href="#" className="pagination-link">3</a>
//                     <button type="button" className="pagination-button" aria-label="Próxima página">
//                         ›
//                     </button>
//                 </nav>
//             </main>
//         </>
//     );
// }export default ListaPorSala;