import { AppRoute } from "@routing";

export const Routes: AppRoute[] = [
  { path: "", data: { alias: "admin-home", title: "Admin | Home" }, loadComponent: () => import("./index/index.component").then(m => m.IndexComponent) },
  { path: "users", data: { alias: "admin-users", title: "Admin | Users" }, loadComponent: () => import("./users/users.component").then(m => m.UsersComponent) },
  { path: "users/:userId", data: { alias: "admin-user", title: "Admin | User" }, loadComponent: () => import("./user/user.component").then(m => m.UserComponent) },
  { path: "roles", data: { alias: "admin-roles", title: "Admin | Roles" }, loadComponent: () => import("./roles/roles.component").then(m => m.RolesComponent) },
  { path: "roles/:role", data: { alias: "admin-role", title: "Admin | Role" }, loadComponent: () => import("./role/role.component").then(m => m.RoleComponent) },
  { path: "claims/:type/:value", data: { alias: "admin-claim", title: "Admin | Claim" }, loadComponent: () => import("./claim/claim.component").then(m => m.ClaimComponent) },
  { path: "articles", data: { alias: "admin-articles", title: "Admin | Articles" }, loadComponent: () => import("./articles/articles.component").then(m => m.ArticlesComponent) },
];
