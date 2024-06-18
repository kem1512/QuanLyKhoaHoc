"use client";

import { BillStatusClient, BillStatusMapping } from "../../web-api-client";
import DataTable from "../../../components/DataTable/DataTable";

export default function DashboardBillStatus() {
  const BillStatusService = new BillStatusClient();

  return (
    <DataTable
      url="/bill-status"
      fields={Object.keys(new BillStatusMapping().toJSON())}
      deleteAction={(id) => BillStatusService.deleteEntity(id)}
      fetchAction={(filters, sorts, page, pageSize) =>
        BillStatusService.getEntities(filters, sorts, page, pageSize)
      }
    />
  );
}
