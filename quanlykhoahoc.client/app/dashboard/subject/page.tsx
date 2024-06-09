"use client";

import DataTable from "../../../components/DataTable/DataTable";
import { SubjectClient, SubjectMapping } from "../../web-api-client";

export default function DashboardSubject() {
  const SubjectService = new SubjectClient();

  return (
    <DataTable
      url="/subject"
      fields={Object.keys(new SubjectMapping().toJSON())}
      deleteAction={(id) => SubjectService.deleteEntity(id)}
      fetchAction={(filters, sorts, page, pageSize) =>
        SubjectService.getEntities(filters, sorts, page, pageSize)
      }
    />
  );
}
