"use client";

import RegisterStudyHandler from "../../../../components/Handler/RegisterStudyHandler";

export default function DashboardRegisterStudyUpdate({
  params,
}: {
  params: { id: number };
}) {
  const { id } = params;

  return <RegisterStudyHandler id={id} />;
}
