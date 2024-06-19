"use client";

import {
  RegisterStudyClient,
  RegisterStudyMapping,
} from "../../web-api-client";
import DataTable from "../../../components/DataTable/DataTable";

export default function DashboardRegisterStudy() {
  const RegisterStudyService = new RegisterStudyClient();

  return (
    <DataTable
      url="/register-study"
      fields={Object.keys(new RegisterStudyMapping().toJSON()).filter(
        (c) => c !== "user" && c !== "course" && c !== "currentSubject"
      )}
      deleteAction={(id) => RegisterStudyService.deleteEntity(id)}
      fetchAction={(filters, sorts, page, pageSize) =>
        RegisterStudyService.getEntities(filters, sorts, page, pageSize)
      }
    />
  );
}
