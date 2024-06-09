"use client";

import DataTable from "../../../components/DataTable/DataTable";
import { RoleClient, RoleMapping } from "../../web-api-client";

export default function DashboardRole() {
  const RoleService = new RoleClient();

  return (
    <DataTable
      url="/role"
      fields={Object.keys(new RoleMapping().toJSON())}
      deleteAction={(id) => RoleService.deleteEntity(id)}
      fetchAction={(filters, sorts, page, pageSize) =>
        RoleService.getEntities(filters, sorts, page, pageSize)
      }
    />
  );
}
