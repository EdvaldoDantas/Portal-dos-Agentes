import api from "@/components/Requisições/Urlapi"
import { auth } from "./Auth"

export async function getTotal(router) {
    try {
        const response = await api.get("api/DashBoardAgente/GetTotal", {
            headers: {
                Authorization: `Bearer ${sessionStorage.getItem('accessToken')}`
            }
        });
        if(response.status == 200) {
            console.log("Dados do usuário obtidos com sucesso.");
        }
        const total = response.data;
        return total;
    } catch (error) {
        if (error.response && error.response.status === 401) {
            if(await auth()) {
                console.warn("Tentando obter o total novamente");
                return getTotal(); // Tenta novamente após renovar o token
            }
            else {
                console.warn("Falha ao renovar o token de acesso. Redirecionando para a página de login..."); // Redireciona para a página de login
                return false
            }
        }   
        console.error("Error fetching user data:", error);
        throw error;
    }
}

export const getRanking = async ()=>{
      try {
        const response = await api.get("/api/DashBoard/GetRankingMes", {
            headers: {
                Authorization: `Bearer ${sessionStorage.getItem('accessToken')}`
            }
        });
        if(response.status == 200) {
            console.log("Dados do usuário obtidos com sucesso.");
        }
        const ranking = response.data;
        return ranking;
    } catch (error) {
        if (error.response && error.response.status === 401) {
            if(await auth()) {
                console.warn("Tentando obter o ranking novamente");
                return getRanking(); // Tenta novamente após renovar o token
            }
            else {
                console.warn("Falha ao renovar o token de acesso. Redirecionando para a página de login..."); // Redireciona para a página de login
                return false
            }
        }   
        console.error("Error fetching user data:", error);
        throw error;
    }
}

export const dashBoardDadosMes = async()=>{
        try {
        const response = await api.get("/api/DashBoardAgente/DashboardDadosMes", {
            headers: {
                Authorization: `Bearer ${sessionStorage.getItem('accessToken')}`
            }
        });
        if(response.status == 200) {
            console.log("Os Dados do gráficos estão sendo carregados");
        }
        return response.data;
    } catch (error) {
        if (error.response && error.response.status === 401) {
            if(await auth()) {
                console.warn("Tentando buscar os dados novamente após renovar");
                return getNewUsers(); // Tenta novamente após renovar o token
            }
            else {
                console.warn("Falha ao renovar o token de acesso. Redirecionando para a página de login..."); // Redireciona para a página de login
                return false
            }
        }   
        console.error("Error fetching user data:", error);
        throw error;
    }
}