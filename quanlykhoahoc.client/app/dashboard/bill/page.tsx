"use client";

import { BillClient, BillMapping } from "../../web-api-client";
import DataTable from "../../../components/DataTable/DataTable";

export default function DashboardBill() {
  const BillService = new BillClient();

  return (
    <DataTable
      url="/bill"
      fields={Object.keys(new BillMapping().toJSON()).filter(
        (c) => c !== "billStatus" && c !== "user" && c !== "course"
      )}
      deleteAction={(id) => BillService.deleteEntity(id)}
      fetchAction={(filters, sorts, page, pageSize) =>
        BillService.getEntities(filters, sorts, page, pageSize)
      }
    />
  );
}
