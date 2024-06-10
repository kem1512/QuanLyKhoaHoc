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
          c !== "provinceId" &&
          c !== "districtId" &&
          c !== "wardId" &&
          c !== "certificate" &&
          c !== "certificateId"
      )}
      deleteAction={(id) => UserService.deleteEntity(id)}
      fetchAction={(filters, sorts, page, pageSize) =>
        UserService.getEntities(filters, sorts, page, pageSize)
      }
    />
  );
}
