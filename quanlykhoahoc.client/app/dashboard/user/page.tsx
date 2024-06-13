"use client";

import DataTable from "../../../components/DataTable/DataTable";
import { UserClient, UserMapping } from "../../web-api-client";

export default function DashboardUser() {
  const UserService = new UserClient();

  return (
    <DataTable
      url="/user"
      fields={Object.keys(new UserMapping().toJSON()).filter(
        (c) =>
          c !== "province" &&
          c !== "district" &&
          c !== "ward" &&
          c !== "certificate"
      )}
      deleteAction={(id) => UserService.deleteEntity(id)}
      fetchAction={(filters, sorts, page, pageSize) =>
        UserService.getEntities(filters, sorts, page, pageSize)
      }
    />
  );
}
