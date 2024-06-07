"use client";

import DataTable from "../../../components/DataTable/DataTable";
import { SubjectClient, SubjectMapping } from "../../web-api-client";

export default function DashboardSubject() {
  const SubjectService = new SubjectClient();

  return (
    <DataTable
      url="/api/Subject"
      fields={Object.keys(new SubjectMapping().toJSON())}
      deleteAction={(id) => SubjectService.deleteSubject(id)}
      fetchAction={(filters, sorts, page, pageSize) =>
        SubjectService.getSubjects(filters, sorts, page, pageSize)
      }
    />
  );
}
