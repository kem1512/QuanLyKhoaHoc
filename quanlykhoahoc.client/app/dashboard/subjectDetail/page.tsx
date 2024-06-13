"use client";

import { SubjectDetailClient, SubjectDetailMapping } from "../../web-api-client";
import DataTable from "../../../components/DataTable/DataTable";

export default function DashboardSubjectDetail() {
  const SubjectDetailService = new SubjectDetailClient();

  return (
    <DataTable
      url="/subjectDetail"
      fields={Object.keys(new SubjectDetailMapping().toJSON()).filter(
        (c) => c !== "creator"
      )}
      deleteAction={(id) => SubjectDetailService.deleteEntity(id)}
      fetchAction={(filters, sorts, page, pageSize) =>
        SubjectDetailService.getEntities(filters, sorts, page, pageSize)
      }
    />
  );
}
